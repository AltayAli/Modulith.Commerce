using Modulith.Commerce.AdminUser.Domain.Roles;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.Roles.Queries.GetRoles
{
    public class GetRolesQueryHandler(IRolesRepository rolesRepository)
        : IQueryHandler<GetRolesQuery, List<GetRolesItemResponse>>
    {
        public async Task<Result<List<GetRolesItemResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var options = new FilteringOptions<Role>();

            if (!string.IsNullOrWhiteSpace(request.Key))
            {
                string key = request.Key.Trim();
                options.Predicates.Add(r => r.Name.Value.Contains(key));
            }

            var roles = await rolesRepository.SelectAsync(options, cancellationToken);

            var response = roles
                .Select(r => new GetRolesItemResponse(
                    r.Id,
                    r.Name.Value,
                    r.IsSystemRole,
                    r.KeycloakRoleName.Value,
                    r.ModifiedDate))
                .ToList();

            return Result.Success(response);
        }
    }
}
