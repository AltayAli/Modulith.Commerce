using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.Departments.Commands.UpdateDepartment
{
    public record UpdateDepartmentCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid HeadId { get; set; }
        public Guid? ParentId { get; set; }
    }
}
