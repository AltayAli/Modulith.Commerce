using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUser.Domain.ActivityLogs
{
    public static class ActivityLogErrors
    {
        public static Error InvalidUserId => new Error("ActivityLog.InvalidUserId", "The provided UserId is invalid.");
        public static Error InvalidAction => new Error("ActivityLog.InvalidAction", "The provided Action is invalid.");
        public static Error InvalidResource => new Error("ActivityLog.InvalidResource", "The provided Resource is invalid.");
        public static Error InvalidIpAddress => new Error("ActivityLog.InvalidIpAddress", "The provided IpAddress is invalid.");
    }
}
