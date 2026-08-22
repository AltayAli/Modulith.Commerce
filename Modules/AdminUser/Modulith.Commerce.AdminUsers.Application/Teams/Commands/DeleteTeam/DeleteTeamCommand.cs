using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.Teams.Commands.DeleteTeam
{
    public record DeleteTeamCommand(Guid Id) : ICommand;
}
