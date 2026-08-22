using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUser.Domain.Roles
{
    public static class RoleErrors
    {
        public static Error MissingId => new Error("Role.MissingId", "The role ID is missing.");
        public static Error MissingRoleName => new Error("Role.MissingRoleName", "The role name is missing.");
        public static Error RoleNameMaxCharacterLimit => new Error("Role.RoleNameMaxCharacterLimit", "Role name exceeds maximum character limit.");
        public static Error RoleDescriptionMaxCharacterLimit => new Error("Role.RoleDescriptionMaxCharacterLimit", "Role description exceeds maximum character limit.");

        public static Error MissingKeycloakRoleName => new Error("Role.MissingKeycloakRoleName", "The Keycloak role name is missing.");
        public static Error KeycloakRoleNameMaxCharacterLimit => new Error("Role.KeycloakRoleNameMaxCharacterLimit", "Keycloak role name exceeds maximum character limit.");
        public static Error NotFound => new Error("Role.NotFound", "Role not found.");
        public static Error AlreadyExists => new Error("Role.AlreadyExists", "A role with this name already exists.");
        public static Error KeycloakSyncFailed => new Error("Role.KeycloakSyncFailed", "Keycloak sync failed for the role.");
    }
}
