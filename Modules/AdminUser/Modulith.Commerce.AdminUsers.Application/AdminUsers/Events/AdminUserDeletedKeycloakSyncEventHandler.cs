using MediatR;
using Microsoft.Extensions.Logging;
using Modulith.Commerce.AdminUser.Domain.AdminUsers.Events;
using Modulith.Commerce.Common.Auth;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Events
{
    public class AdminUserDeletedKeycloakSyncEventHandler(
    IKeycloakClient keycloakClient,
    ILogger<AdminUserDeletedKeycloakSyncEventHandler> logger)
    : INotificationHandler<AdminUserDeletedEvent>
    {
        public async Task Handle(AdminUserDeletedEvent notification, CancellationToken cancellationToken)
        {
            if (notification.KeycloakId is null)
                return;

            try
            {
                await keycloakClient.DeleteUserAsync(notification.KeycloakId.Value, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Keycloak user deletion failed for AdminUserId {AdminUserId}", notification.AdminUserId);
            }
        }
    }
}
