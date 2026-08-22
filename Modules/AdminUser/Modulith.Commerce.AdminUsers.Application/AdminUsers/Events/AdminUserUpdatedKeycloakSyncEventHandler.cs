using MediatR;
using Microsoft.Extensions.Logging;
using Modulith.Commerce.AdminUser.Domain.AdminUsers;
using Modulith.Commerce.AdminUser.Domain.AdminUsers.Events;
using Modulith.Commerce.Common.Auth;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Events
{
    public class AdminUserUpdatedKeycloakSyncEventHandler(
    IKeycloakClient keycloakClient,
    ILogger<AdminUserUpdatedKeycloakSyncEventHandler> logger)
    : INotificationHandler<AdminUserUpdatedEvent>
    {
        public async Task Handle(AdminUserUpdatedEvent notification, CancellationToken cancellationToken)
        {
            if (notification.KeycloakId is null)
                return;

            try
            {
                bool enabled = notification.Status is not (AdminUserStatus.Suspended or AdminUserStatus.Offboarded);

                await keycloakClient.UpdateUserAsync(
                    notification.KeycloakId.Value,
                    notification.FirstName,
                    notification.LastName,
                    enabled,
                    cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Keycloak user update failed for AdminUserId {AdminUserId}", notification.AdminUserId);
            }
        }
    }
}
