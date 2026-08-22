using Modulith.Commerce.AdminUser.Domain.AdminUsers;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Queries.GetAdminUser
{
    public class GetAdminUserQueryHandler(IAdminUsersRepository adminUsersRepository)
        : IQueryHandler<GetAdminUserQuery, GetAdminUserResponse?>
    {
        public async Task<Result<GetAdminUserResponse?>> Handle(GetAdminUserQuery request, CancellationToken cancellationToken)
        {
            var adminUser = await adminUsersRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser>
            {
                Predicates = [u => u.Id == request.Id]
            }, cancellationToken);

            if (adminUser is null)
                return Result.Failure<GetAdminUserResponse?>(null, AdminUserErrors.NotFound);

            return Result.Success<GetAdminUserResponse?>(new GetAdminUserResponse(
                adminUser.Id,
                adminUser.KeyCloakId,
                adminUser.Email.Value,
                adminUser.FirstName.Value,
                adminUser.LastName.Value,
                adminUser.Title.Value,
                adminUser.TeamId,
                adminUser.Status,
                adminUser.PhoneNumber?.Number,
                adminUser.AvatarUrl?.Url,
                adminUser.ContractStartDate,
                adminUser.ContractEndDate,
                adminUser.OffboardedAt,
                adminUser.MfaEnabled));
        }
    }
}
