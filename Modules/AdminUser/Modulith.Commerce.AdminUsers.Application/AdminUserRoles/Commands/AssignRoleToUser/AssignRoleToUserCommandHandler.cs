using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.AdminUserRoles;
using Modulith.Commerce.AdminUser.Domain.Roles;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.Common.Domain.Abstractions;
using AdminUserEntity = Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser;
using IAdminUsersRepository = Modulith.Commerce.AdminUser.Domain.AdminUsers.IAdminUsersRepository;

namespace Modulith.Commerce.AdminUsers.Application.AdminUserRoles.Commands.AssignRoleToUser
{
    public class AssignRoleToUserCommandHandler(
        IAdminUsersRepository adminUsersRepository,
        IRolesRepository rolesRepository,
        IAdminUserRolesRepository adminUserRolesRepository,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork)
        : ICommandHandler<AssignRoleToUserCommand>
    {
        public async Task<Result> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await adminUsersRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<AdminUserEntity>
            {
                Predicates = [u => u.Id == request.UserId]
            }, cancellationToken);

            if (user is null)
                return Result.Failure(AdminUserRoleErrors.UserNotFound);

            var role = await rolesRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Role>
            {
                Predicates = [r => r.Id == request.RoleId]
            }, cancellationToken);

            if (role is null)
                return Result.Failure(AdminUserRoleErrors.RoleNotFound);

            var now = dateTimeProvider.UtcNow;

            bool alreadyAssigned = await adminUserRolesRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<AdminUserRole>
            {
                Predicates =
                [
                    ur => ur.UserId == request.UserId && ur.RoleId == request.RoleId,
                    ur => ur.ExpiredAt == null || ur.ExpiredAt > now
                ]
            }, cancellationToken) is not null;

            if (alreadyAssigned)
                return Result.Failure(AdminUserRoleErrors.AlreadyExists);

            var adminUserRole = AdminUserRole.Create(request.UserId, request.RoleId, request.ExpiredAt, request.Reason);
            await adminUserRolesRepository.InsertAsync(adminUserRole, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
