using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Products.Domain.StaffMembers
{
    public static class StaffMemberErrors
    {
        public static Error NotFound => new Error("StaffMember.NotFound", "Staff member not found.");
    }
}
