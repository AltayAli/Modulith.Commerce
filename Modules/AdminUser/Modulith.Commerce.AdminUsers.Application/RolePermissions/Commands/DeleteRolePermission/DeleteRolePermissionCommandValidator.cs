using FluentValidation;
using Modulith.Commerce.AdminUser.Domain.RolePermissions;

namespace Modulith.Commerce.AdminUsers.Application.RolePermissions.Commands.DeleteRolePermission
{
    public class DeleteRolePermissionCommandValidator : AbstractValidator<DeleteRolePermissionCommand>
    {
        public DeleteRolePermissionCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(RolePermissionErrors.MissingId.Code);
        }
    }
}
