using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using AdminUserErrors = Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUserErrors;
using AdminUserEntity = Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser;
using IAdminUsersRepository = Modulith.Commerce.AdminUser.Domain.AdminUsers.IAdminUsersRepository;

namespace Modulith.Commerce.AdminUsers.Application.Teams.Commands.RemoveUserFromTeam
{
    public class RemoveUserFromTeamCommandHandler(
        IAdminUsersRepository adminUsersRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<RemoveUserFromTeamCommand>
    {
        public async Task<Result> Handle(RemoveUserFromTeamCommand request, CancellationToken cancellationToken)
        {
            var user = await adminUsersRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<AdminUserEntity>
            {
                Predicates = [u => u.Id == request.UserId]
            }, cancellationToken);

            if (user is null)
                return Result.Failure(AdminUserErrors.NotFound);

            if (user.TeamId is null)
                return Result.Failure(AdminUserErrors.NotInTeam);

            user.RemoveFromTeam();
            await adminUsersRepository.UpdateAsync(user, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
