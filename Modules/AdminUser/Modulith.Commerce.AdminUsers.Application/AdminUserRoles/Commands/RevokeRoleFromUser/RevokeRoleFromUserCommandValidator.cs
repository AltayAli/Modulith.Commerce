using FluentValidation;
using Modulith.Commerce.AdminUser.Domain.AdminUserRoles;

namespace Modulith.Commerce.AdminUsers.Application.AdminUserRoles.Commands.RevokeRoleFromUser
{
    public class RevokeRoleFromUserCommandValidator : AbstractValidator<RevokeRoleFromUserCommand>
    {
        public RevokeRoleFromUserCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage(AdminUserRoleErrors.MissingUserId.Code);
            RuleFor(x => x.RoleId).NotEmpty().WithMessage(AdminUserRoleErrors.MissingRoleId.Code);
        }
    }
}
