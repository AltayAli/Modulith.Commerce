using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUser.Domain.Departments
{
    public static class DepartmentErrors
    {
        public static Error NotFound => new Error("Department.NotFound", "Department not found.");
        public static Error AlreadyExists => new Error("Department.AlreadyExists", "A department with this name already exists.");
        public static Error MissingId => new Error("Department.MissingId", "The department ID is missing.");
        public static Error MissingName => new Error("Department.MissingName", "The department name is missing.");
        public static Error MissingHeadId => new Error("Department.MissingHeadId", "The department head ID is missing.");
    }
}
