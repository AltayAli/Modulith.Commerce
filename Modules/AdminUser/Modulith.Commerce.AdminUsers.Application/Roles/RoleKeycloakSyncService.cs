using Microsoft.Extensions.Logging;
using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.Roles;
using Modulith.Commerce.Common.Auth;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.Roles
{
    public interface IRoleKeycloakSyncService
    {
        Task SyncAsync(Role role, CancellationToken cancellationToken);
    }

    public class RoleKeycloakSyncService(
        IKeycloakClient keycloakClient,
        IRolesRepository rolesRepository,
        IUnitOfWork unitOfWork,
        ILogger<RoleKeycloakSyncService> logger)
        : IRoleKeycloakSyncService
    {
        public async Task SyncAsync(Role role, CancellationToken cancellationToken)
        {
            try
            {
                await keycloakClient.CreateRoleAsync(role.KeycloakRoleName.Value, role.Description?.Value, cancellationToken);

                role.MarkKeycloakSyncCompleted();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Keycloak role sync failed for RoleId {RoleId}", role.Id);

                role.MarkKeycloakSyncFailed();
            }

            await rolesRepository.UpdateAsync(role, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
