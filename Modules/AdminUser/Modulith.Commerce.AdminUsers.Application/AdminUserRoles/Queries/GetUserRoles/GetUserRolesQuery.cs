using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.AdminUserRoles.Queries.GetUserRoles
{
    public record GetUserRolesQuery(Guid UserId) : IQuery<List<GetUserRolesItemResponse>>;
}
