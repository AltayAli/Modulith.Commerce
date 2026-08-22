using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.Roles.Commands.SyncRole
{
    public record SyncRoleCommand(Guid Id) : ICommand;
}
