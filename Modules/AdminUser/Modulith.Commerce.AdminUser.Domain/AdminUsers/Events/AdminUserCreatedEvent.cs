using Newtonsoft.Json;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUser.Domain.AdminUsers.Events
{
    public sealed record AdminUserCreatedEvent(
    Guid AdminUserId,
    string Email,
    string FirstName,
    string LastName,
    AdminUserStatus Status,
    Guid? TeamId,
    [property: JsonIgnore] string Password) : IDomainEvent;
}
