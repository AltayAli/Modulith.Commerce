using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.Roles;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler
        (IRolesRepository rolesRepository,
        IUnitOfWork unitOfWork) : ICommandHandler<UpdateRoleCommand>
    {
        public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await rolesRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Role>
            {
                Predicates = [r => r.Id == request.Id]
            }, cancellationToken);

            if (role is null)
                return Result.Failure(RoleErrors.NotFound);

            string trimmedName = request.Name.Trim().ToLower();

            bool nameExists = await rolesRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Role>
            {
                Predicates = [r => r.Name.Value.ToLower() == trimmedName && r.Id != request.Id]
            }, cancellationToken) is not null;

            if (nameExists)
                return Result.Failure(RoleErrors.AlreadyExists);

            role.Update(request.Name, request.Description, request.IsSystemRole, request.KeycloakRoleName);

            await rolesRepository.UpdateAsync(role, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
