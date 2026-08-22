namespace Modulith.Commerce.AdminUsers.Presentation.AdminUserRoles.DTOs
{
    public record AssignRoleToUserRequestDto
    {
        public Guid RoleId { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public string? Reason { get; set; }
    }
}
