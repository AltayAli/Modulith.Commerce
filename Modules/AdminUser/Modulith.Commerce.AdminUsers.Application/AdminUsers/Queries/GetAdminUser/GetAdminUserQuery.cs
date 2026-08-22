using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Queries.GetAdminUser
{
    public record GetAdminUserQuery(Guid Id) : IQuery<GetAdminUserResponse?>;
}
