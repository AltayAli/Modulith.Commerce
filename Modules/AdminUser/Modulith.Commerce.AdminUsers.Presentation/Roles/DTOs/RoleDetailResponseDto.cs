namespace Modulith.Commerce.AdminUsers.Presentation.Roles.DTOs
{
    public record RoleDetailResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsSystemRole { get; set; }
        public string? Description { get; set; }
        public string KeycloakRoleName { get; set; }
    }
}
