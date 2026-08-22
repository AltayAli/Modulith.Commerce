using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.Teams.Commands.RemoveUserFromTeam
{
    public record RemoveUserFromTeamCommand(Guid UserId) : ICommand;
}
