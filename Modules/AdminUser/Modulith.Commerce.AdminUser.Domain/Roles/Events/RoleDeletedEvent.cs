using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUser.Domain.Roles.Events
{
    public sealed record RoleDeletedEvent(Guid RoleId, string KeycloakRoleName) : IDomainEvent;
}
