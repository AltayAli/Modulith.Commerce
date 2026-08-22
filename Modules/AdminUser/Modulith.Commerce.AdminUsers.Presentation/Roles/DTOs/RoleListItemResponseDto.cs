namespace Modulith.Commerce.AdminUsers.Presentation.Roles.DTOs
{
    public record RoleListItemResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsSystemRole { get; set; }
        public string KeycloakRoleName { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
