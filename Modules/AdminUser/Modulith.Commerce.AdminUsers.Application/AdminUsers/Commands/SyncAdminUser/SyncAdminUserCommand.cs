using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Commands.SyncAdminUser
{
    public record SyncAdminUserCommand(Guid Id) : ICommand;
}
