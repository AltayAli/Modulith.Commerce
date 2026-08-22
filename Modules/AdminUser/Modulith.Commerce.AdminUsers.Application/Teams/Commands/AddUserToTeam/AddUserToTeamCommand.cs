using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.Teams.Commands.AddUserToTeam
{
    public record AddUserToTeamCommand(Guid UserId, Guid TeamId) : ICommand;
}
