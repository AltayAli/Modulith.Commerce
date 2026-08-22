namespace Modulith.Commerce.AdminUsers.Infrastructure.Bootstrap
{
    public class BootstrapAdministratorOptions
    {
        public const string SectionName = "Bootstrap";

        public string? AdministratorEmail { get; set; }

        public string AdministratorFirstName { get; set; } = "System";

        public string AdministratorLastName { get; set; } = "Administrator";

        public string? AdministratorPassword { get; set; }
    }
}
