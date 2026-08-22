using FluentValidation;
using Modulith.Commerce.AdminUser.Domain.Departments;

namespace Modulith.Commerce.AdminUsers.Application.Departments.Commands.AddDepartment
{
    public class AddDepartmentCommandValidator : AbstractValidator<AddDepartmentCommand>
    {
        public AddDepartmentCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(DepartmentErrors.MissingName.Code);
            RuleFor(x => x.HeadId).NotEmpty().WithMessage(DepartmentErrors.MissingHeadId.Code);
        }
    }
}
