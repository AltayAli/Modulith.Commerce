using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUser.Domain.AdminUsers.Events
{
    public sealed record AdminUserDeletedEvent(
    Guid AdminUserId,
    string Email,
    Guid? KeycloakId) : IDomainEvent;
}
