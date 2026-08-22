using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.RolePermissions.Commands.AddRolePermission
{
    public record AddRolePermissionCommand : ICommand
    {
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
    }
}
