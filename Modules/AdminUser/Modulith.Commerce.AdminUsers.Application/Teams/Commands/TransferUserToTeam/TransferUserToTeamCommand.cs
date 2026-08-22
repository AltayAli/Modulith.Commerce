using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.Teams.Commands.TransferUserToTeam
{
    public record TransferUserToTeamCommand(Guid UserId, Guid TeamId) : ICommand;
}
