using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUser.Domain.AdminUsers
{
    public static class AdminUserErrors
    {
        public static Error NotFound => new Error("AdminUser.NotFound", "Admin user not found.");
        public static Error MissingId => new Error("AdminUser.MissingId", "The admin user ID is missing.");
        public static Error AlreadyInTeam => new Error("AdminUser.AlreadyInTeam", "The user is already assigned to a team.");
        public static Error NotInTeam => new Error("AdminUser.NotInTeam", "The user is not assigned to any team.");
        public static Error EmailAlreadyExists => new Error("AdminUser.EmailAlreadyExists", "An admin user with this email already exists.");
        public static Error MissingEmail => new Error("AdminUser.MissingEmail", "The email is missing.");
        public static Error InvalidEmail => new Error("AdminUser.InvalidEmail", "The email format is invalid.");
        public static Error EmailMaxCharacterLimit => new Error("AdminUser.EmailMaxCharacterLimit", "Email exceeds maximum character limit.");
        public static Error MissingFirstName => new Error("AdminUser.MissingFirstName", "The first name is missing.");
        public static Error FirstNameMaxCharacterLimit => new Error("AdminUser.FirstNameMaxCharacterLimit", "First name exceeds maximum character limit.");
        public static Error MissingLastName => new Error("AdminUser.MissingLastName", "The last name is missing.");
        public static Error LastNameMaxCharacterLimit => new Error("AdminUser.LastNameMaxCharacterLimit", "Last name exceeds maximum character limit.");
        public static Error MissingTitle => new Error("AdminUser.MissingTitle", "The title is missing.");
        public static Error TitleMaxCharacterLimit => new Error("AdminUser.TitleMaxCharacterLimit", "Title exceeds maximum character limit.");
        public static Error MissingContractStartDate => new Error("AdminUser.MissingContractStartDate", "The contract start date is missing.");
        public static Error InvalidStatus => new Error("AdminUser.InvalidStatus", "The admin user status is invalid.");
        public static Error MissingPassword => new Error("AdminUser.MissingPassword", "The password is missing.");
        public static Error PasswordMinLength => new Error("AdminUser.PasswordMinLength", "The password must be at least 8 characters long.");
        public static Error KeycloakSyncFailed => new Error("AdminUser.KeycloakSyncFailed", "Keycloak sync failed for the admin user.");
    }
}
