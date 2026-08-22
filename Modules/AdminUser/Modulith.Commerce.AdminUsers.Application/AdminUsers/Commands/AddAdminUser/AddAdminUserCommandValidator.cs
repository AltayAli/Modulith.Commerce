using FluentValidation;
using Modulith.Commerce.AdminUser.Domain.AdminUsers;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Commands.AddAdminUser
{
    public class AddAdminUserCommandValidator : AbstractValidator<AddAdminUserCommand>
    {
        public AddAdminUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(AdminUserErrors.MissingEmail.Code)
                .EmailAddress()
                .WithMessage(AdminUserErrors.InvalidEmail.Code)
                .MaximumLength(300)
                .WithMessage(AdminUserErrors.EmailMaxCharacterLimit.Code);

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

            RuleFor(x => x.ContractStartDate)
                .NotEmpty()
                .WithMessage(AdminUserErrors.MissingContractStartDate.Code);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(AdminUserErrors.MissingPassword.Code)
                .MinimumLength(8)
                .WithMessage(AdminUserErrors.PasswordMinLength.Code);
        }
    }
}
