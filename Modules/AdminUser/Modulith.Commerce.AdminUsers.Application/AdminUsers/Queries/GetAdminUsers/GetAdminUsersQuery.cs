using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Queries.GetAdminUsers
{
    public record GetAdminUsersQuery(string? Key) : IQuery<List<GetAdminUsersItemResponse>>;
}
