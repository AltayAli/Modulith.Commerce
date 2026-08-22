namespace Modulith.Commerce.AdminUsers.Application.RolePermissions.Queries.GetRolePermissions
{
    public record GetRolePermissionsItemResponse(
        Guid Id,
        Guid PermissionId,
        string PermissionName,
        string Policy,
        string Description);
}
