namespace Modulith.Commerce.AdminUsers.Application.Roles.Queries.GetRole
{
    public record GetRoleResponse(
        Guid Id,
        string Name,
        bool IsSystemRole,
        string? Description,
        string KeycloakRoleName);
}
