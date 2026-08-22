using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.Roles;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.Roles.Commands.AddRole
{
    public class AddRoleCommandHandler
        (IRolesRepository rolesRepository,
        IUnitOfWork unitOfWork,
        IRoleKeycloakSyncService keycloakSyncService)
        : ICommandHandler<AddRoleCommand>
    {
        public async Task<Result> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            string trimmedName = request.Name.Trim().ToLower();

            var existingRole = await rolesRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Role>
            {
                Predicates = [r => r.Name.Value.ToLower() == trimmedName]
            }, cancellationToken);

            if (existingRole is not null)
            {
                if (existingRole.Status != RoleStatus.KeycloakSyncFailed)
                    return Result.Failure(RoleErrors.AlreadyExists);

                existingRole.RetryKeycloakSync(
                    request.Name,
                    request.Description,
                    request.IsSystemRole,
                    request.KeycloakRoleName);

                await keycloakSyncService.SyncAsync(existingRole, cancellationToken);

                if (existingRole.Status == RoleStatus.KeycloakSyncFailed)
                    return Result.Failure(RoleErrors.KeycloakSyncFailed);

                return Result.Success();
            }

            var role = Role.Create(
                    request.Name,
                    request.Description,
                    request.IsSystemRole,
                    request.KeycloakRoleName);

            await rolesRepository.InsertAsync(role, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            await keycloakSyncService.SyncAsync(role, cancellationToken);

            if (role.Status == RoleStatus.KeycloakSyncFailed)
                return Result.Failure(RoleErrors.KeycloakSyncFailed);

            return Result.Success();
        }
    }
}
