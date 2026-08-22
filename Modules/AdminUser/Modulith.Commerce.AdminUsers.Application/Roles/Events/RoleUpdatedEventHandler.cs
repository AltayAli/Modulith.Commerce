using MediatR;
using Microsoft.Extensions.Logging;
using Modulith.Commerce.AdminUser.Domain.Roles.Events;
using Modulith.Commerce.Common.Auth;

namespace Modulith.Commerce.AdminUsers.Application.Roles.Events
{
    public class RoleUpdatedEventHandler(
        IKeycloakClient keycloakClient,
        ILogger<RoleUpdatedEventHandler> logger)
        : INotificationHandler<RoleUpdatedEvent>
    {
        public async Task Handle(RoleUpdatedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                await keycloakClient.UpdateRoleAsync(
                    notification.OldKeycloakRoleName,
                    notification.NewKeycloakRoleName,
                    notification.Description,
                    cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Keycloak role update failed for RoleId {RoleId}", notification.RoleId);
            }
        }
    }
}
