using Microsoft.Extensions.Logging;
using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.AdminUsers;
using Modulith.Commerce.Common.Auth;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers
{
    public interface IAdminUserKeycloakSyncService
    {
        Task SyncAsync(Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser adminUser, string password, CancellationToken cancellationToken);
    }

    public class AdminUserKeycloakSyncService(
        IKeycloakClient keycloakClient,
        IAdminUsersRepository adminUsersRepository,
        IUnitOfWork unitOfWork,
        ILogger<AdminUserKeycloakSyncService> logger)
        : IAdminUserKeycloakSyncService
    {
        public async Task SyncAsync(Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser adminUser, string password, CancellationToken cancellationToken)
        {
            try
            {
                Guid keycloakUserId = await keycloakClient.CreateUserAsync(
                    new KeycloakUserCreateRequest(adminUser.Email.Value, adminUser.FirstName.Value, adminUser.LastName.Value, password),
                    cancellationToken);

                adminUser.SetKeycloakId(keycloakUserId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Keycloak sync failed for AdminUserId {AdminUserId}", adminUser.Id);

                adminUser.MarkKeycloakSyncFailed();
            }

            await adminUsersRepository.UpdateAsync(adminUser, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
