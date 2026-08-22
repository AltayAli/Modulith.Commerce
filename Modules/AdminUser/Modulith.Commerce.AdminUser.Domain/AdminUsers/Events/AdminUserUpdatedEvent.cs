using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUser.Domain.AdminUsers.Events
{
    public sealed record AdminUserUpdatedEvent(
    Guid AdminUserId,
    string Email,
    string FirstName,
    string LastName,
    AdminUserStatus Status,
    Guid? TeamId,
    Guid? KeycloakId) : IDomainEvent;
}
