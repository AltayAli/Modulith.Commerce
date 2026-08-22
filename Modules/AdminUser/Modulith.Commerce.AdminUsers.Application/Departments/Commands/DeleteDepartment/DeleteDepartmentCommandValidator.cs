using FluentValidation;
using Modulith.Commerce.AdminUser.Domain.Departments;

namespace Modulith.Commerce.AdminUsers.Application.Departments.Commands.DeleteDepartment
{
    public class DeleteDepartmentCommandValidator : AbstractValidator<DeleteDepartmentCommand>
    {
        public DeleteDepartmentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(DepartmentErrors.MissingId.Code);
        }
    }
}
