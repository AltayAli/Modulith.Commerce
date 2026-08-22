using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Modulith.Commerce.AdminUser.Domain.AdminUsers.Events;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.AdminUsers.IntegrationEvents.AdminUsers;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Events
{
    public class AdminUserDeletedIntegrationEventHandler(
    IPublishEndpoint publishEndpoint,
    IDateTimeProvider dateTimeProvider,
    ILogger<AdminUserDeletedIntegrationEventHandler> logger)
    : INotificationHandler<AdminUserDeletedEvent>
    {
        public async Task Handle(AdminUserDeletedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                await publishEndpoint.Publish(new AdminUserDeletedIntegrationEvent(
                    notification.AdminUserId,
                    dateTimeProvider.UtcNow), cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to publish AdminUserDeletedIntegrationEvent for AdminUserId {AdminUserId}", notification.AdminUserId);
            }
        }
    }
}
