using Modulith.Commerce.AdminUser.Domain.Roles;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.Roles.Queries.GetRole
{
    public class GetRoleQueryHandler(IRolesRepository rolesRepository)
        : IQueryHandler<GetRoleQuery, GetRoleResponse?>
    {
        public async Task<Result<GetRoleResponse?>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var role = await rolesRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Role>
            {
                Predicates = [r => r.Id == request.Id]
            }, cancellationToken);

            if (role is null)
                return Result.Failure<GetRoleResponse?>(null, RoleErrors.NotFound);

            return Result.Success<GetRoleResponse?>(new GetRoleResponse(
                role.Id,
                role.Name.Value,
                role.IsSystemRole,
                role.Description?.Value,
                role.KeycloakRoleName.Value));
        }
    }
}
