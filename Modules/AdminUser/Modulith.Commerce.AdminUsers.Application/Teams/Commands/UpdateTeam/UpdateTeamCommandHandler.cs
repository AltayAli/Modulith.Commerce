using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.Teams;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.Teams.Commands.UpdateTeam
{
    public class UpdateTeamCommandHandler(
        ITeamsRepository teamsRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<UpdateTeamCommand>
    {
        public async Task<Result> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            var team = await teamsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Team>
            {
                Predicates = [t => t.Id == request.Id]
            }, cancellationToken);

            if (team is null)
                return Result.Failure(TeamErrors.NotFound);

            string trimmedName = request.Name.Trim().ToLower();

            bool nameExists = await teamsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Team>
            {
                Predicates =
                [
                    t => t.DepartmentId == request.DepartmentId &&
                         t.Name.Value.ToLower() == trimmedName &&
                         t.Id != request.Id
                ]
            }, cancellationToken) is not null;

            if (nameExists)
                return Result.Failure(TeamErrors.AlreadyExists);

            team.Update(request.Name, request.DepartmentId, request.LeadId);
            await teamsRepository.UpdateAsync(team, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
