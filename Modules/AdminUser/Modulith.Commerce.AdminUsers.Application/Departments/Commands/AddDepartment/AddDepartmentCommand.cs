using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.Departments.Commands.AddDepartment
{
    public record AddDepartmentCommand : ICommand
    {
        public string Name { get; set; }
        public Guid HeadId { get; set; }
        public Guid? ParentId { get; set; }
    }
}
