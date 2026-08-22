using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.Teams;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using AdminUserErrors = Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUserErrors;
using AdminUserEntity = Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser;
using IAdminUsersRepository = Modulith.Commerce.AdminUser.Domain.AdminUsers.IAdminUsersRepository;

namespace Modulith.Commerce.AdminUsers.Application.Teams.Commands.AddUserToTeam
{
    public class AddUserToTeamCommandHandler(
        IAdminUsersRepository adminUsersRepository,
        ITeamsRepository teamsRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<AddUserToTeamCommand>
    {
        public async Task<Result> Handle(AddUserToTeamCommand request, CancellationToken cancellationToken)
        {
            var user = await adminUsersRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<AdminUserEntity>
            {
                Predicates = [u => u.Id == request.UserId]
            }, cancellationToken);

            if (user is null)
                return Result.Failure(AdminUserErrors.NotFound);

            if (user.TeamId is not null)
                return Result.Failure(AdminUserErrors.AlreadyInTeam);

            bool teamExists = await teamsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Team>
            {
                Predicates = [t => t.Id == request.TeamId]
            }, cancellationToken) is not null;

            if (!teamExists)
                return Result.Failure(TeamErrors.NotFound);

            user.AssignToTeam(request.TeamId);
            await adminUsersRepository.UpdateAsync(user, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
