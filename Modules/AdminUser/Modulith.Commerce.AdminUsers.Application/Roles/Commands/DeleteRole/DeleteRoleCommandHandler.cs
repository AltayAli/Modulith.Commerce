using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.Roles;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler(
        IRolesRepository rolesRepository,
        IUnitOfWork unitOfWork) : ICommandHandler<DeleteRoleCommand>
    {
        public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await rolesRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Role>
            {
                Predicates = [r => r.Id == request.Id]
            }, cancellationToken);

            if (role is null)
                return Result.Failure(RoleErrors.NotFound);

            await rolesRepository.DeleteAsync(role, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
