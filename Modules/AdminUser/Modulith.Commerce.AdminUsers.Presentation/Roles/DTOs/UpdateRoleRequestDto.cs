namespace Modulith.Commerce.AdminUsers.Presentation.Roles.DTOs
{
    public record UpdateRoleRequestDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsSystemRole { get; set; }
        public string KeycloakRoleName { get; set; }
    }
}
