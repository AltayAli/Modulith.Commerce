using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.Roles.Commands.DeleteRole
{
    public record DeleteRoleCommand(Guid Id) : ICommand;
}
