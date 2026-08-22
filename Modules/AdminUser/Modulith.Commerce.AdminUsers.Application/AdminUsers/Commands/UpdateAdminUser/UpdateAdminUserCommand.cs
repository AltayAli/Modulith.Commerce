using Modulith.Commerce.AdminUser.Domain.AdminUsers;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Commands.UpdateAdminUser
{
    public record UpdateAdminUserCommand : ICommand
    {
        public Guid Id { get; set; }
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
