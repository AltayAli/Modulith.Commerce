namespace Modulith.Commerce.AdminUsers.Presentation.AdminUsers.DTOs
{
    public record AddAdminUserRequestDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime ContractStartDate { get; set; }
        public bool MfaEnabled { get; set; }
        public string Password { get; set; }
    }
}
