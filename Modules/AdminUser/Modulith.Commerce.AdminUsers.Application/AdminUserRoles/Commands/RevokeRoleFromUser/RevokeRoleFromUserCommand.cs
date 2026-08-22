using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.AdminUserRoles.Commands.RevokeRoleFromUser
{
    public record RevokeRoleFromUserCommand(Guid UserId, Guid RoleId) : ICommand;
}
