namespace Modulith.Commerce.AdminUsers.Presentation.RolePermissions.DTOs
{
    public record RolePermissionListItemResponseDto
    {
        public Guid Id { get; set; }
        public Guid PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string Policy { get; set; }
        public string Description { get; set; }
    }
}
