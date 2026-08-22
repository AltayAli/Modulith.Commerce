using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.Teams;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.Teams.Commands.DeleteTeam
{
    public class DeleteTeamCommandHandler(
        ITeamsRepository teamsRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<DeleteTeamCommand>
    {
        public async Task<Result> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            var team = await teamsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Team>
            {
                Predicates = [t => t.Id == request.Id]
            }, cancellationToken);

            if (team is null)
                return Result.Failure(TeamErrors.NotFound);

            await teamsRepository.DeleteAsync(team, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
