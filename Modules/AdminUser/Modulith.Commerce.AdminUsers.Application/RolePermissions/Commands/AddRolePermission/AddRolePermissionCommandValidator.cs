using FluentValidation;
using Modulith.Commerce.AdminUser.Domain.RolePermissions;

namespace Modulith.Commerce.AdminUsers.Application.RolePermissions.Commands.AddRolePermission
{
    public class AddRolePermissionCommandValidator : AbstractValidator<AddRolePermissionCommand>
    {
        public AddRolePermissionCommandValidator()
        {
            RuleFor(x => x.RoleId).NotEmpty().WithMessage(RolePermissionErrors.MissingRoleId.Code);
            RuleFor(x => x.PermissionId).NotEmpty().WithMessage(RolePermissionErrors.MissingPermissionId.Code);
        }
    }
}
