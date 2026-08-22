using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUser.Domain.AdminUserRoles
{
    public static class AdminUserRoleErrors
    {
        public static Error NotFound => new Error("AdminUserRole.NotFound", "Role assignment not found.");
        public static Error AlreadyExists => new Error("AdminUserRole.AlreadyExists", "This role is already assigned to the user.");
        public static Error UserNotFound => new Error("AdminUserRole.UserNotFound", "The admin user was not found.");
        public static Error RoleNotFound => new Error("AdminUserRole.RoleNotFound", "The role was not found.");
        public static Error MissingId => new Error("AdminUserRole.MissingId", "The role assignment ID is missing.");
        public static Error MissingUserId => new Error("AdminUserRole.MissingUserId", "The admin user ID is missing.");
        public static Error MissingRoleId => new Error("AdminUserRole.MissingRoleId", "The role ID is missing.");
    }
}
