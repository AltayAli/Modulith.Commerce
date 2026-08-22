using Modulith.Commerce.AdminUser.Domain.AdminUserRoles;
using Modulith.Commerce.AdminUser.Domain.RolePermissions;
using Modulith.Commerce.AdminUser.Domain.Roles.Events;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Common.Domain.ValueObjects;

namespace Modulith.Commerce.AdminUser.Domain.Roles
{
    public class Role : BaseEntity
    {
        private Role()
        {
            UserRoles = new HashSet<AdminUserRole>();
            RolePermissions = new HashSet<RolePermission>();
        }
        public Text Name { get; private set; }
        public Text? Description { get; private set; }
        public bool IsSystemRole { get; private set; }
        public Text KeycloakRoleName { get; private set; }
        public RoleStatus Status { get; private set; }
        public ICollection<AdminUserRole> UserRoles { get; private set; }
        public ICollection<RolePermission> RolePermissions { get; private set; }

        public static Role Create(string name, string? description, bool isSystemRole, string keycloakRoleName)
        {
            var role = new Role
            {
                Name = (Text)name,
                Description = description != null ? (Text)description : null,
                IsSystemRole = isSystemRole,
                KeycloakRoleName = (Text)keycloakRoleName,
                Status = RoleStatus.PendingKeycloakSync
            };

            return role;
        }

        public Role Update(string name, string? description, bool isSystemRole, string keycloakRoleName)
        {
            string oldKeycloakRoleName = KeycloakRoleName.Value;

            Name = (Text)name;
            Description = description != null ? (Text)description : null;
            IsSystemRole = isSystemRole;
            KeycloakRoleName = (Text)keycloakRoleName;

            AddDomainEvent(new RoleUpdatedEvent(Id, oldKeycloakRoleName, KeycloakRoleName.Value, Description?.Value));

            return this;
        }

        public void MarkKeycloakSyncCompleted()
        {
            Status = RoleStatus.Active;
        }

        public void MarkKeycloakSyncFailed()
        {
            Status = RoleStatus.KeycloakSyncFailed;
        }

        public Role RetryKeycloakSync(string name, string? description, bool isSystemRole, string keycloakRoleName)
        {
            Name = (Text)name;
            Description = description != null ? (Text)description : null;
            IsSystemRole = isSystemRole;
            KeycloakRoleName = (Text)keycloakRoleName;
            Status = RoleStatus.PendingKeycloakSync;

            return this;
        }

        public override void MarkAsDeleted(Guid? deletedBy, DateTime deletedDate)
        {
            base.MarkAsDeleted(deletedBy, deletedDate);
            AddDomainEvent(new RoleDeletedEvent(Id, KeycloakRoleName.Value));
        }
    }
}
