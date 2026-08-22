using Modulith.Commerce.AdminUser.Domain.AdminUsers;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Queries.GetAdminUsers
{
    public record GetAdminUsersItemResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Title,
        AdminUserStatus Status,
        Guid? TeamId);
}
