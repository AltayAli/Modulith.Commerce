using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.Roles;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Auth;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.Roles.Commands.SyncRole
{
    public class SyncRoleCommandHandler(
        IRolesRepository rolesRepository,
        IKeycloakClient keycloakClient,
        IUnitOfWork unitOfWork,
        IRoleKeycloakSyncService keycloakSyncService)
        : ICommandHandler<SyncRoleCommand>
    {
        public async Task<Result> Handle(SyncRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await rolesRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Role>
            {
                Predicates = [r => r.Id == request.Id]
            }, cancellationToken);

            if (role is null)
                return Result.Failure(RoleErrors.NotFound);

            var exists = await keycloakClient.RoleExistsAsync(role.KeycloakRoleName.Value, cancellationToken);

            if (exists)
            {
                role.MarkKeycloakSyncCompleted();

                await rolesRepository.UpdateAsync(role, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }

            role.RetryKeycloakSync(
                role.Name.Value,
                role.Description?.Value,
                role.IsSystemRole,
                role.KeycloakRoleName.Value);

            await keycloakSyncService.SyncAsync(role, cancellationToken);

            if (role.Status == RoleStatus.KeycloakSyncFailed)
                return Result.Failure(RoleErrors.KeycloakSyncFailed);

            return Result.Success();
        }
    }
}
