using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.Roles.Queries.GetRole
{
    public record GetRoleQuery(Guid Id) : IQuery<GetRoleResponse?>;
}
