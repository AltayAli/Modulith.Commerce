using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.AdminUsers;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Commands.AddAdminUser
{
    public class AddAdminUserCommandHandler
        (IAdminUsersRepository adminUsersRepository,
        IUnitOfWork unitOfWork,
        IAdminUserKeycloakSyncService keycloakSyncService)
        : ICommandHandler<AddAdminUserCommand>
    {
        public async Task<Result> Handle(AddAdminUserCommand request, CancellationToken cancellationToken)
        {
            string trimmedEmail = request.Email.Trim().ToLower();

            var existingAdminUser = await adminUsersRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser>
            {
                Predicates = [u => u.Email.Value.ToLower() == trimmedEmail]
            }, cancellationToken);

            if (existingAdminUser is not null)
            {
                if (existingAdminUser.Status != AdminUserStatus.KeycloakSyncFailed)
                    return Result.Failure(AdminUserErrors.EmailAlreadyExists);

                existingAdminUser.RetryKeycloakSync(
                    request.MfaEnabled);

                await keycloakSyncService.SyncAsync(existingAdminUser, request.Password, cancellationToken);

                if (existingAdminUser.Status == AdminUserStatus.KeycloakSyncFailed)
                    return Result.Failure(AdminUserErrors.KeycloakSyncFailed);

                return Result.Success();
            }

            var adminUser = Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser.Create(
                request.Email,
                request.FirstName,
                request.LastName,
                request.Title,
                request.PhoneNumber,
                request.AvatarUrl,
                request.ContractStartDate,
                request.MfaEnabled,
                request.Password);

            await adminUsersRepository.InsertAsync(adminUser, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
