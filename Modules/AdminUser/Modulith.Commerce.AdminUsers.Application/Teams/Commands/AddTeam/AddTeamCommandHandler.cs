using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.Teams;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.Teams.Commands.AddTeam
{
    public class AddTeamCommandHandler(
        ITeamsRepository teamsRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<AddTeamCommand>
    {
        public async Task<Result> Handle(AddTeamCommand request, CancellationToken cancellationToken)
        {
            string trimmedName = request.Name.Trim().ToLower();

            bool nameExists = await teamsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Team>
            {
                Predicates =
                [
                    t => t.DepartmentId == request.DepartmentId &&
                         t.Name.Value.ToLower() == trimmedName
                ]
            }, cancellationToken) is not null;

            if (nameExists)
                return Result.Failure(TeamErrors.AlreadyExists);

            var team = Team.Create(request.Name, request.DepartmentId, request.LeadId);
            await teamsRepository.InsertAsync(team, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
