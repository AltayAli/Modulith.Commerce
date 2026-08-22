using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.AdminUserRoles.Commands.AssignRoleToUser
{
    public record AssignRoleToUserCommand : ICommand
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public string? Reason { get; set; }
    }
}
