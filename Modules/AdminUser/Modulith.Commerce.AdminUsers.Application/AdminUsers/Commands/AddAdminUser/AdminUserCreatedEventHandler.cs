using MediatR;
using Microsoft.Extensions.Logging;
using Modulith.Commerce.AdminUser.Domain.AdminUsers;
using Modulith.Commerce.AdminUser.Domain.AdminUsers.Events;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Commands.AddAdminUser
{
    public class AdminUserCreatedEventHandler(
    IAdminUsersRepository adminUsersRepository,
    IAdminUserKeycloakSyncService keycloakSyncService,
    ILogger<AdminUserCreatedEventHandler> logger)
    : INotificationHandler<AdminUserCreatedEvent>
    {
        public async Task Handle(AdminUserCreatedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var adminUser = await adminUsersRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser>
                {
                    Predicates = [u => u.Id == notification.AdminUserId]
                }, cancellationToken);

                if (adminUser is null)
                    return;

                await keycloakSyncService.SyncAsync(adminUser, notification.Password, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to sync AdminUserId {AdminUserId} to Keycloak after creation", notification.AdminUserId);
            }
        }
    }
}
