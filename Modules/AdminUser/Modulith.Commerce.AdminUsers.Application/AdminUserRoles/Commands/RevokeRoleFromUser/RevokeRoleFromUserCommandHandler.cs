using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.AdminUserRoles;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.AdminUserRoles.Commands.RevokeRoleFromUser
{
    public class RevokeRoleFromUserCommandHandler(
        IAdminUserRolesRepository adminUserRolesRepository,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork)
        : ICommandHandler<RevokeRoleFromUserCommand>
    {
        public async Task<Result> Handle(RevokeRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            var now = dateTimeProvider.UtcNow;

            var adminUserRole = await adminUserRolesRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<AdminUserRole>
            {
                Predicates =
                [
                    ur => ur.UserId == request.UserId && ur.RoleId == request.RoleId,
                    ur => ur.ExpiredAt == null || ur.ExpiredAt > now
                ]
            }, cancellationToken);

            if (adminUserRole is null)
                return Result.Failure(AdminUserRoleErrors.NotFound);

            await adminUserRolesRepository.DeleteAsync(adminUserRole, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
