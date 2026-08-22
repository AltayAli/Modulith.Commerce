using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUser.Domain.RolePermissions
{
    public static class RolePermissionErrors
    {
        public static Error NotFound => new Error("RolePermission.NotFound", "Role permission not found.");
        public static Error AlreadyExists => new Error("RolePermission.AlreadyExists", "This permission is already assigned to the role.");
        public static Error MissingId => new Error("RolePermission.MissingId", "The role permission ID is missing.");
        public static Error MissingRoleId => new Error("RolePermission.MissingRoleId", "The role ID is missing.");
        public static Error MissingPermissionId => new Error("RolePermission.MissingPermissionId", "The permission ID is missing.");
    }
}
