using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.Permissions;
using Modulith.Commerce.AdminUser.Domain.RolePermissions;
using Modulith.Commerce.AdminUser.Domain.Roles;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.RolePermissions.Commands.AddRolePermission
{
    public class AddRolePermissionCommandHandler(
        IRolesRepository rolesRepository,
        IPermissionsRepository permissionsRepository,
        IRolePermissionsRepository rolePermissionsRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<AddRolePermissionCommand>
    {
        public async Task<Result> Handle(AddRolePermissionCommand request, CancellationToken cancellationToken)
        {
            var role = await rolesRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Role>
            {
                Predicates = [r => r.Id == request.RoleId]
            }, cancellationToken);

            if (role is null)
                return Result.Failure(RoleErrors.NotFound);

            var permission = await permissionsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Permission>
            {
                Predicates = [p => p.Id == request.PermissionId]
            }, cancellationToken);

            if (permission is null)
                return Result.Failure(PermissionErrors.NotFound);

            bool alreadyAssigned = await rolePermissionsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<RolePermission>
            {
                Predicates = [rp => rp.RoleId == request.RoleId && rp.PermissionId == request.PermissionId]
            }, cancellationToken) is not null;

            if (alreadyAssigned)
                return Result.Failure(RolePermissionErrors.AlreadyExists);

            var rolePermission = RolePermission.Create(request.RoleId, request.PermissionId);
            await rolePermissionsRepository.InsertAsync(rolePermission, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
