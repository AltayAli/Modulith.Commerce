using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Products.Domain.StaffMembers
{
    public sealed class StaffMember : BaseEntity
    {
        private StaffMember()
        {
        }

        public string Email { get; private set; }
        public string FullName { get; private set; }
        public string Status { get; private set; }
        public Guid? TeamId { get; private set; }

        public static StaffMember CreateFrom(Guid adminUserId, string email, string fullName, string status, Guid? teamId)
        {
            return new StaffMember
            {
                Id = adminUserId,
                Email = email,
                FullName = fullName,
                Status = status,
                TeamId = teamId
            };
        }

        public StaffMember ApplyUpdate(string email, string fullName, string status, Guid? teamId)
        {
            Email = email;
            FullName = fullName;
            Status = status;
            TeamId = teamId;

            return this;
        }
    }
}
