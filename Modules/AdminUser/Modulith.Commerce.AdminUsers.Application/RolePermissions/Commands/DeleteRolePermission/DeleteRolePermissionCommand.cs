using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.RolePermissions.Commands.DeleteRolePermission
{
    public record DeleteRolePermissionCommand(Guid Id) : ICommand;
}
