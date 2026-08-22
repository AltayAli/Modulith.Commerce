namespace Modulith.Commerce.AdminUsers.Presentation.AdminUserRoles.DTOs
{
    public record UserRoleListItemResponseDto
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public string? Reason { get; set; }
    }
}
