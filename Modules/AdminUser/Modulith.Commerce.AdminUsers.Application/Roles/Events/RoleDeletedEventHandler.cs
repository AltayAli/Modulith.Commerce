using MediatR;
using Microsoft.Extensions.Logging;
using Modulith.Commerce.AdminUser.Domain.Roles.Events;
using Modulith.Commerce.Common.Auth;

namespace Modulith.Commerce.AdminUsers.Application.Roles.Events
{
    public class RoleDeletedEventHandler(
        IKeycloakClient keycloakClient,
        ILogger<RoleDeletedEventHandler> logger)
        : INotificationHandler<RoleDeletedEvent>
    {
        public async Task Handle(RoleDeletedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                await keycloakClient.DeleteRoleAsync(notification.KeycloakRoleName, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Keycloak role deletion failed for RoleId {RoleId}", notification.RoleId);
            }
        }
    }
}
