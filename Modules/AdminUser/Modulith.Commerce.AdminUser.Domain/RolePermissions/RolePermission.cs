using Modulith.Commerce.AdminUser.Domain.Permissions;
using Modulith.Commerce.AdminUser.Domain.Roles;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUser.Domain.RolePermissions
{
    public class RolePermission : BaseEntity
    {
        private RolePermission() { }

        public Guid RoleId { get; private set; }
        public Role Role { get; private set; }
        public Guid PermissionId { get; private set; }
        public Permission Permission { get; private set; }

        public static RolePermission Create(Guid roleId, Guid permissionId)
        {
            return new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId
            };
        }
    }
}
