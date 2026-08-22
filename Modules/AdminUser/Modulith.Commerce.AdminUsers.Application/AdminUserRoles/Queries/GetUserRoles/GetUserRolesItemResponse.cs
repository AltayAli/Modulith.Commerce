namespace Modulith.Commerce.AdminUsers.Application.AdminUserRoles.Queries.GetUserRoles
{
    public record GetUserRolesItemResponse(
        Guid Id,
        Guid RoleId,
        string RoleName,
        DateTime? ExpiredAt,
        string? Reason);
}
