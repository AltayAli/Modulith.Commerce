using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.Products.Application.StaffMembers.Commands.UpsertStaffMember
{
    public record UpsertStaffMemberCommand : ICommand
    {
        public Guid AdminUserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
        public Guid? TeamId { get; set; }
    }
}
