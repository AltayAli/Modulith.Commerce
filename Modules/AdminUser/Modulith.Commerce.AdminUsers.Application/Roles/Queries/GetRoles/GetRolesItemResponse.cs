namespace Modulith.Commerce.AdminUsers.Application.Roles.Queries.GetRoles
{
    public record GetRolesItemResponse(
        Guid Id,
        string Name,
        bool IsSystemRole,
        string KeycloakRoleName,
        DateTime? LastModifiedDate);
}
