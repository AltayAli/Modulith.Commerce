using MassTransit;
using MediatR;
using Modulith.Commerce.AdminUsers.IntegrationEvents.AdminUsers;
using Modulith.Commerce.Products.Application.StaffMembers.Commands.UpsertStaffMember;

namespace Modulith.Commerce.Products.Infrastructure.Messaging.Consumers
{
    public class AdminUserUpdatedConsumer(ISender sender) : IConsumer<AdminUserUpdatedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<AdminUserUpdatedIntegrationEvent> context)
        {
            await sender.Send(new UpsertStaffMemberCommand
            {
                AdminUserId = context.Message.AdminUserId,
                Email = context.Message.Email,
                FullName = context.Message.FullName,
                Status = context.Message.Status,
                TeamId = context.Message.TeamId
            }, context.CancellationToken);
        }
    }
}
