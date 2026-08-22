using Modulith.Commerce.AdminUser.Domain.AdminUserRoles;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.AdminUserRoles.Queries.GetUserRoles
{
    public class GetUserRolesQueryHandler(
        IAdminUserRolesRepository adminUserRolesRepository,
        IDateTimeProvider dateTimeProvider)
        : IQueryHandler<GetUserRolesQuery, List<GetUserRolesItemResponse>>
    {
        public async Task<Result<List<GetUserRolesItemResponse>>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            var now = dateTimeProvider.UtcNow;

            var userRoles = await adminUserRolesRepository.SelectAsync(new FilteringOptions<AdminUserRole>
            {
                Predicates =
                [
                    ur => ur.UserId == request.UserId,
                    ur => ur.ExpiredAt == null || ur.ExpiredAt > now
                ],
                Relations = ["Role"]
            }, cancellationToken);

            var response = userRoles
                .Select(ur => new GetUserRolesItemResponse(
                    ur.Id,
                    ur.RoleId,
                    ur.Role.Name.Value,
                    ur.ExpiredAt,
                    ur.Reason != null ? ur.Reason.Value : null))
                .ToList();

            return Result.Success(response);
        }
    }
}
