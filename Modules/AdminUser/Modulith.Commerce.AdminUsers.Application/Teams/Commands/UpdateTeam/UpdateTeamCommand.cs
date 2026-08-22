using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.Teams.Commands.UpdateTeam
{
    public record UpdateTeamCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid LeadId { get; set; }
    }
}
