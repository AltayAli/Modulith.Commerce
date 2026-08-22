using MassTransit;
using MediatR;
using Modulith.Commerce.AdminUsers.IntegrationEvents.AdminUsers;
using Modulith.Commerce.Products.Application.StaffMembers.Commands.RemoveStaffMember;

namespace Modulith.Commerce.Products.Infrastructure.Messaging.Consumers
{
    public class AdminUserDeletedConsumer(ISender sender) : IConsumer<AdminUserDeletedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<AdminUserDeletedIntegrationEvent> context)
        {
            await sender.Send(new RemoveStaffMemberCommand
            {
                AdminUserId = context.Message.AdminUserId
            }, context.CancellationToken);
        }
    }
}
