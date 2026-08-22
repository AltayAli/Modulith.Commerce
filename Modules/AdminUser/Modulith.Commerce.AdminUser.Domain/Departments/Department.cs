using Modulith.Commerce.AdminUser.Domain.Teams;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Common.Domain.ValueObjects;

namespace Modulith.Commerce.AdminUser.Domain.Departments
{
    public class Department : BaseEntity
    {
        private Department()
        {
            Teams = new HashSet<Team>();
        }

        public Guid? ParentId { get; private set; }
        public Department? Parent { get; private set; }
        public Text Name { get; private set; }
        public Guid HeadId { get; private set; }
        public AdminUsers.AdminUser Head { get; private set; }
        public ICollection<Team> Teams { get; private set; }

        public static Department Create(string name, Guid headId, Guid? parentId = null)
        {
            return new Department
            {
                Name = (Text)name,
                HeadId = headId,
                ParentId = parentId
            };
        }

        public Department Update(string name, Guid headId, Guid? parentId)
        {
            Name = (Text)name;
            HeadId = headId;
            ParentId = parentId;
            return this;
        }
    }
}
