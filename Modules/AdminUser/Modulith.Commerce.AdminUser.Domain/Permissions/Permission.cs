using Modulith.Commerce.AdminUser.Domain.RolePermissions;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Common.Domain.ValueObjects;

namespace Modulith.Commerce.AdminUser.Domain.Permissions
{
    public class Permission : BaseEntity
    {
        private Permission()
        {
            RolePermissions = new HashSet<RolePermission>();
        }
        public Text Name { get; private set; }
        public Text Description { get; private set; }
        public Text Policy { get; private set; }
        public ICollection<RolePermission> RolePermissions { get; private set; }

        public static Permission Create(string name, string description, string policy)
        {
            var permission = new Permission
            {
                Name = (Text)name,
                Description = (Text)description,
                Policy = (Text)policy
            };

            return permission;
        }
    }
}
