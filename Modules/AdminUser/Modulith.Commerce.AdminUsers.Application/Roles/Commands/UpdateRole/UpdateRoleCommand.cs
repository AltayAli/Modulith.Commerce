using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.Roles.Commands.UpdateRole
{
    public record UpdateRoleCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsSystemRole { get; set; }
        public string KeycloakRoleName { get; set; }
    }
}
