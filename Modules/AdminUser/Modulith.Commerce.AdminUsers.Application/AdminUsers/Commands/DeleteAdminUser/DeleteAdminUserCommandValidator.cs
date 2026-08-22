using FluentValidation;
using Modulith.Commerce.AdminUser.Domain.AdminUsers;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Commands.DeleteAdminUser
{
    public class DeleteAdminUserCommandValidator : AbstractValidator<DeleteAdminUserCommand>
    {
        public DeleteAdminUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(AdminUserErrors.MissingId.Code);
        }
    }
}
