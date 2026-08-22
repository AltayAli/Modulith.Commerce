using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.Teams.Commands.AddTeam
{
    public record AddTeamCommand : ICommand
    {
        public string Name { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid LeadId { get; set; }
    }
}
