using Modulith.Commerce.AdminUser.Domain.AdminUsers;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Queries.GetAdminUsers
{
    public class GetAdminUsersQueryHandler(IAdminUsersRepository adminUsersRepository)
        : IQueryHandler<GetAdminUsersQuery, List<GetAdminUsersItemResponse>>
    {
        public async Task<Result<List<GetAdminUsersItemResponse>>> Handle(GetAdminUsersQuery request, CancellationToken cancellationToken)
        {
            var options = new FilteringOptions<Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser>();

            if (!string.IsNullOrWhiteSpace(request.Key))
            {
                string key = request.Key.Trim();
                options.Predicates.Add(u =>
                    u.FirstName.Value.Contains(key) ||
                    u.LastName.Value.Contains(key) ||
                    u.Email.Value.Contains(key));
            }

            var adminUsers = await adminUsersRepository.SelectAsync(options, cancellationToken);

            var response = adminUsers
                .Select(u => new GetAdminUsersItemResponse(
                    u.Id,
                    u.FirstName.Value,
                    u.LastName.Value,
                    u.Email.Value,
                    u.Title.Value,
                    u.Status,
                    u.TeamId))
                .ToList();

            return Result.Success(response);
        }
    }
}
