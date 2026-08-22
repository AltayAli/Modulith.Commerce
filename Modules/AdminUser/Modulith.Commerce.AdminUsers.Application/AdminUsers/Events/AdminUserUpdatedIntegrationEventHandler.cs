using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Modulith.Commerce.AdminUser.Domain.AdminUsers.Events;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.AdminUsers.IntegrationEvents.AdminUsers;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Events
{
    public class AdminUserUpdatedIntegrationEventHandler(
    IPublishEndpoint publishEndpoint,
    IDateTimeProvider dateTimeProvider,
    ILogger<AdminUserUpdatedIntegrationEventHandler> logger)
    : INotificationHandler<AdminUserUpdatedEvent>
    {
        public async Task Handle(AdminUserUpdatedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                await publishEndpoint.Publish(new AdminUserUpdatedIntegrationEvent(
                    notification.AdminUserId,
                    notification.Email,
                    $"{notification.FirstName} {notification.LastName}",
                    notification.Status.ToString(),
                    notification.TeamId,
                    dateTimeProvider.UtcNow), cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to publish AdminUserUpdatedIntegrationEvent for AdminUserId {AdminUserId}", notification.AdminUserId);
            }
        }
    }
}
