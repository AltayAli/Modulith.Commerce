using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.AdminUsers.Commands.DeleteAdminUser
{
    public record DeleteAdminUserCommand(Guid Id) : ICommand;
}
