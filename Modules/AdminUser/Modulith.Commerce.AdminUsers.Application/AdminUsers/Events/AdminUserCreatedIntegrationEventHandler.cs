using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Modulith.Commerce.AdminUser.Domain.AdminUsers.Events;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.AdminUsers.IntegrationEvents.AdminUsers;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Events
{
    public class AdminUserCreatedIntegrationEventHandler(
    IPublishEndpoint publishEndpoint,
    IDateTimeProvider dateTimeProvider,
    ILogger<AdminUserCreatedIntegrationEventHandler> logger)
    : INotificationHandler<AdminUserCreatedEvent>
    {
        public async Task Handle(AdminUserCreatedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                await publishEndpoint.Publish(new AdminUserCreatedIntegrationEvent(
                    notification.AdminUserId,
                    notification.Email,
                    $"{notification.FirstName} {notification.LastName}",
                    notification.Status.ToString(),
                    notification.TeamId,
                    dateTimeProvider.UtcNow), cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to publish AdminUserCreatedIntegrationEvent for AdminUserId {AdminUserId}", notification.AdminUserId);
            }
        }
    }
}
