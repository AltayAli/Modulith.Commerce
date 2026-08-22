namespace Modulith.Commerce.AdminUsers.Presentation.Authorization
{

    public static class AdminUserPolicies
    {
        public const string AdminUserRead = "adminuser:read";
        public const string AdminUserWrite = "adminuser:write";
        public const string AdminUserDelete = "adminuser:delete";

        public const string DepartmentRead = "department:read";
        public const string DepartmentWrite = "department:write";
        public const string DepartmentDelete = "department:delete";

        public const string RoleRead = "role:read";
        public const string RoleWrite = "role:write";
        public const string RoleDelete = "role:delete";

        public const string RolePermissionRead = "rolepermission:read";
        public const string RolePermissionWrite = "rolepermission:write";
        public const string RolePermissionDelete = "rolepermission:delete";

        public const string AdminUserRoleRead = "adminuserrole:read";
        public const string AdminUserRoleWrite = "adminuserrole:write";
        public const string AdminUserRoleDelete = "adminuserrole:delete";

        public const string TeamWrite = "team:write";
        public const string TeamDelete = "team:delete";
    }
}
