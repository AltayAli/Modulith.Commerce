using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.Roles.Queries.GetRoles
{
    public record GetRolesQuery(string? Key) : IQuery<List<GetRolesItemResponse>>;
}
