using Modulith.Commerce.AdminUser.Domain.AdminUsers;

namespace Modulith.Commerce.AdminUsers.Presentation.AdminUsers.DTOs
{
    public record AdminUserDetailResponseDto
    {
        public Guid Id { get; set; }
        public Guid? KeyCloakId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public Guid? TeamId { get; set; }
        public AdminUserStatus Status { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public DateTime? OffboardedAt { get; set; }
        public bool MfaEnabled { get; set; }
    }
}
