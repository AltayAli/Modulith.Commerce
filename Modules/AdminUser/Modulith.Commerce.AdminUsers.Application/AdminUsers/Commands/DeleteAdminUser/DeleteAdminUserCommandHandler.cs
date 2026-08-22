using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.AdminUsers;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Commands.DeleteAdminUser
{
    public class DeleteAdminUserCommandHandler(
        IAdminUsersRepository adminUsersRepository,
        IUnitOfWork unitOfWork) : ICommandHandler<DeleteAdminUserCommand>
    {
        public async Task<Result> Handle(DeleteAdminUserCommand request, CancellationToken cancellationToken)
        {
            var adminUser = await adminUsersRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser>
            {
                Predicates = [u => u.Id == request.Id]
            }, cancellationToken);

            if (adminUser is null)
                return Result.Failure(AdminUserErrors.NotFound);

            await adminUsersRepository.DeleteAsync(adminUser, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
