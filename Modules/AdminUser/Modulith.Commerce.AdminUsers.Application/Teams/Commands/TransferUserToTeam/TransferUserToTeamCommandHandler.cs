using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.Teams;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using AdminUserErrors = Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUserErrors;
using AdminUserEntity = Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser;
using IAdminUsersRepository = Modulith.Commerce.AdminUser.Domain.AdminUsers.IAdminUsersRepository;

namespace Modulith.Commerce.AdminUsers.Application.Teams.Commands.TransferUserToTeam
{
    public class TransferUserToTeamCommandHandler(
        IAdminUsersRepository adminUsersRepository,
        ITeamsRepository teamsRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<TransferUserToTeamCommand>
    {
        public async Task<Result> Handle(TransferUserToTeamCommand request, CancellationToken cancellationToken)
        {
            var user = await adminUsersRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<AdminUserEntity>
            {
                Predicates = [u => u.Id == request.UserId]
            }, cancellationToken);

            if (user is null)
                return Result.Failure(AdminUserErrors.NotFound);

            if (user.TeamId is null)
                return Result.Failure(AdminUserErrors.NotInTeam);

            bool teamExists = await teamsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Team>
            {
                Predicates = [t => t.Id == request.TeamId]
            }, cancellationToken) is not null;

            if (!teamExists)
                return Result.Failure(TeamErrors.NotFound);

            user.TransferToTeam(request.TeamId);
            await adminUsersRepository.UpdateAsync(user, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
