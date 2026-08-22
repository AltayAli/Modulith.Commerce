using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUser.Domain.Permissions
{
    public static class PermissionErrors
    {
        public static Error NotFound => new Error("Permission.NotFound", "Permission not found.");
        public static Error MissingId => new Error("Permission.MissingId", "The permission ID is missing.");
    }
}
