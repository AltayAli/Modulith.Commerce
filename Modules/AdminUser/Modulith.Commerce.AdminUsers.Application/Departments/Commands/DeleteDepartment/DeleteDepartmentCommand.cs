using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.Departments.Commands.DeleteDepartment
{
    public record DeleteDepartmentCommand(Guid Id) : ICommand;
}
