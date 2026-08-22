using System.Security.Cryptography;
using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.AdminUsers;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Auth;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Commands.SyncAdminUser
{
    public class SyncAdminUserCommandHandler(
        IAdminUsersRepository adminUsersRepository,
        IKeycloakClient keycloakClient,
        IUnitOfWork unitOfWork,
        IAdminUserKeycloakSyncService keycloakSyncService)
        : ICommandHandler<SyncAdminUserCommand>
    {
        public async Task<Result> Handle(SyncAdminUserCommand request, CancellationToken cancellationToken)
        {
            var adminUser = await adminUsersRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser>
            {
                Predicates = [u => u.Id == request.Id]
            }, cancellationToken);

            if (adminUser is null)
                return Result.Failure(AdminUserErrors.NotFound);

            var keycloakUserId = await keycloakClient.FindUserIdAsync(adminUser.Email.Value, cancellationToken);

            if (keycloakUserId is not null)
            {
                adminUser.SetKeycloakId(keycloakUserId.Value);

                await adminUsersRepository.UpdateAsync(adminUser, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }

            var generatedPassword = Convert.ToBase64String(RandomNumberGenerator.GetBytes(12));

            adminUser.RetryKeycloakSync(adminUser.MfaEnabled);

            await keycloakSyncService.SyncAsync(adminUser, generatedPassword, cancellationToken);

            if (adminUser.Status == AdminUserStatus.KeycloakSyncFailed)
                return Result.Failure(AdminUserErrors.KeycloakSyncFailed);

            return Result.Success();
        }
    }
}
