using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.Products.Application.StaffMembers.Commands.RemoveStaffMember
{
    public record RemoveStaffMemberCommand : ICommand
    {
        public Guid AdminUserId { get; set; }
    }
}
