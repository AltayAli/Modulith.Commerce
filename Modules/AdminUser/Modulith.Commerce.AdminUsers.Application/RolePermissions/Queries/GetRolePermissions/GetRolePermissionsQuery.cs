using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.RolePermissions.Queries.GetRolePermissions
{
    public record GetRolePermissionsQuery(Guid RoleId) : IQuery<List<GetRolePermissionsItemResponse>>;
}
