using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.RolePermissions;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.RolePermissions.Commands.DeleteRolePermission
{
    public class DeleteRolePermissionCommandHandler(
        IRolePermissionsRepository rolePermissionsRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<DeleteRolePermissionCommand>
    {
        public async Task<Result> Handle(DeleteRolePermissionCommand request, CancellationToken cancellationToken)
        {
            var rolePermission = await rolePermissionsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<RolePermission>
            {
                Predicates = [rp => rp.Id == request.Id]
            }, cancellationToken);

            if (rolePermission is null)
                return Result.Failure(RolePermissionErrors.NotFound);

            await rolePermissionsRepository.DeleteAsync(rolePermission, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
