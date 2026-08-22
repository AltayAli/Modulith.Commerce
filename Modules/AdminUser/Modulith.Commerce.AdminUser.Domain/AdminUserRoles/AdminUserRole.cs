using Modulith.Commerce.AdminUser.Domain.Roles;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Common.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modulith.Commerce.AdminUser.Domain.AdminUserRoles
{
    public class AdminUserRole : BaseEntity
    {
        private AdminUserRole() { }

        public Guid UserId { get; private set; }
        public AdminUsers.AdminUser User { get; private set; }
        public Guid RoleId { get; private set; }
        public Role Role { get; private set; }
        public DateTime? ExpiredAt { get; private set; }
        public Text? Reason { get; private set; }

        public static AdminUserRole Create(Guid userId, Guid roleId, DateTime? expiredAt, string? reason)
        {
            return new AdminUserRole
            {
                UserId = userId,
                RoleId = roleId,
                ExpiredAt = expiredAt,
                Reason = string.IsNullOrWhiteSpace(reason) ? null : (Text)reason
            };
        }
    }
}
