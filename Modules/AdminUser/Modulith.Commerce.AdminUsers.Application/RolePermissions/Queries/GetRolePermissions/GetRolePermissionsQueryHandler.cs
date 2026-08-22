using Modulith.Commerce.AdminUser.Domain.RolePermissions;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.RolePermissions.Queries.GetRolePermissions
{
    public class GetRolePermissionsQueryHandler(IRolePermissionsRepository rolePermissionsRepository)
        : IQueryHandler<GetRolePermissionsQuery, List<GetRolePermissionsItemResponse>>
    {
        public async Task<Result<List<GetRolePermissionsItemResponse>>> Handle(GetRolePermissionsQuery request, CancellationToken cancellationToken)
        {
            var rolePermissions = await rolePermissionsRepository.SelectAsync(new FilteringOptions<RolePermission>
            {
                Predicates = [rp => rp.RoleId == request.RoleId],
                Relations = ["Permission"]
            }, cancellationToken);

            var response = rolePermissions
                .Select(rp => new GetRolePermissionsItemResponse(
                    rp.Id,
                    rp.PermissionId,
                    rp.Permission.Name.Value,
                    rp.Permission.Policy.Value,
                    rp.Permission.Description.Value))
                .ToList();

            return Result.Success(response);
        }
    }
}
