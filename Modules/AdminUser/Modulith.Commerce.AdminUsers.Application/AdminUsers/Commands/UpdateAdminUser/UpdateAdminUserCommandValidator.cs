using FluentValidation;
using Modulith.Commerce.AdminUser.Domain.AdminUsers;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Commands.UpdateAdminUser
{
    public class UpdateAdminUserCommandValidator : AbstractValidator<UpdateAdminUserCommand>
    {
        public UpdateAdminUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(AdminUserErrors.MissingId.Code);

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(AdminUserErrors.MissingFirstName.Code)
                .MaximumLength(100)
                .WithMessage(AdminUserErrors.FirstNameMaxCharacterLimit.Code);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage(AdminUserErrors.MissingLastName.Code)
                .MaximumLength(100)
                .WithMessage(AdminUserErrors.LastNameMaxCharacterLimit.Code);

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage(AdminUserErrors.MissingTitle.Code)
                .MaximumLength(200)
                .WithMessage(AdminUserErrors.TitleMaxCharacterLimit.Code);

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage(AdminUserErrors.InvalidStatus.Code);
        }
    }
}
