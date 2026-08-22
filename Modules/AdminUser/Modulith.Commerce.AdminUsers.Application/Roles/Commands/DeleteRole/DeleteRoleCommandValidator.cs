using FluentValidation;
using Modulith.Commerce.AdminUser.Domain.Roles;

namespace Modulith.Commerce.AdminUsers.Application.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(RoleErrors.MissingId.Code);
        }
    }
}
