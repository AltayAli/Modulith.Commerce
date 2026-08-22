using FluentValidation;
using Modulith.Commerce.AdminUser.Domain.Departments;

namespace Modulith.Commerce.AdminUsers.Application.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(DepartmentErrors.MissingId.Code);
            RuleFor(x => x.Name).NotEmpty().WithMessage(DepartmentErrors.MissingName.Code);
            RuleFor(x => x.HeadId).NotEmpty().WithMessage(DepartmentErrors.MissingHeadId.Code);
        }
    }
}
