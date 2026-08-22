using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUser.Domain.Teams
{
    public static class TeamErrors
    {
        public static Error NotFound => new Error("Team.NotFound", "Team not found.");
        public static Error AlreadyExists => new Error("Team.AlreadyExists", "A team with this name already exists in this department.");
        public static Error MissingId => new Error("Team.MissingId", "The team ID is missing.");
        public static Error MissingName => new Error("Team.MissingName", "The team name is missing.");
        public static Error MissingDepartmentId => new Error("Team.MissingDepartmentId", "The department ID is missing.");
        public static Error MissingLeadId => new Error("Team.MissingLeadId", "The team lead ID is missing.");
    }
}
