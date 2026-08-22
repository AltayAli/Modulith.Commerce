using System;
using System.Collections.Generic;
using System.Text;

namespace Modulith.Commerce.AdminUser.Domain.AdminUsers
{
    public enum AdminUserStatus
    {
        Active,
        Suspended,
        PendindOffboarding,
        Offboarded,
        PendingKeycloakSync,
        KeycloakSyncFailed
    }
}
