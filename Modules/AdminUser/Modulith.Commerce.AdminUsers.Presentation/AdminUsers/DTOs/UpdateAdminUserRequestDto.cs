using Modulith.Commerce.AdminUser.Domain.AdminUsers;

namespace Modulith.Commerce.AdminUsers.Presentation.AdminUsers.DTOs
{
    public record UpdateAdminUserRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AvatarUrl { get; set; }
        public AdminUserStatus Status { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public bool MfaEnabled { get; set; }
    }
}
