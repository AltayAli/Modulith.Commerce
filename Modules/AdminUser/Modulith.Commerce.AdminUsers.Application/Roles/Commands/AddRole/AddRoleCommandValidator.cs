using FluentValidation;
using Modulith.Commerce.AdminUser.Domain.Roles;

namespace Modulith.Commerce.AdminUsers.Application.Roles.Commands.AddRole
{
    public class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
    {
        public AddRoleCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(RoleErrors.MissingRoleName.Code)
                .MaximumLength(100)
                .WithMessage(RoleErrors.RoleNameMaxCharacterLimit.Code);

            RuleFor(x => x.Description)
                .MaximumLength(250)
                .WithMessage(RoleErrors.RoleDescriptionMaxCharacterLimit.Code);

            RuleFor(x => x.KeycloakRoleName)
                .NotEmpty()
                .WithMessage(RoleErrors.MissingKeycloakRoleName.Code)
                .MaximumLength(100)
                .WithMessage(RoleErrors.KeycloakRoleNameMaxCharacterLimit.Code);
        }
    }
}
