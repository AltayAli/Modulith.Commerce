using Modulith.Commerce.AdminUser.Domain.AdminUsers;

namespace Modulith.Commerce.AdminUsers.Presentation.AdminUsers.DTOs
{
    public record AdminUserListItemResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public AdminUserStatus Status { get; set; }
        public Guid? TeamId { get; set; }
    }
}
