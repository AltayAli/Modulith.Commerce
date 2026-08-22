using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUser.Domain.Roles.Events
{
    public sealed record RoleUpdatedEvent(Guid RoleId, string OldKeycloakRoleName, string NewKeycloakRoleName, string? Description) : IDomainEvent;
}
