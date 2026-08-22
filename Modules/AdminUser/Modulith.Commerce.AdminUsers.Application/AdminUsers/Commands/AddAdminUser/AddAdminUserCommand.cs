using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Commands.AddAdminUser
{
    public record AddAdminUserCommand : ICommand
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
