using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Common.Domain.ValueObjects;

namespace Modulith.Commerce.AdminUser.Domain.ActivityLogs
{
    public class ActivityLog : BaseEntity
    {
        private ActivityLog()
        {

        }
        public Guid UserId { get; private set; }
        public Permissions.Action Action { get; private set; }
        public Permissions.Resource Resource { get; private set; }
        public Text OldValue { get; private set; }
        public Text NewValue { get; private set; }
        public Text IpAddress { get; private set; }
        public Text UserAgent { get; private set; }
        public Text KeycloakSessionId { get; private set; }
        public Text CorellationId { get; private set; }

        public static ActivityLog Create(Guid userId,
                                         string action,
                                         string resource,
                                         string oldValue,
                                         string newValue,
                                         string ipAddress,
                                         string userAgent,
                                         string keycloakSessionId,
                                         string corellationId)
        {
            var activityLog = new ActivityLog
            {
                UserId = userId,
                Action = new Permissions.Action(action),
                Resource = new Permissions.Resource(resource),
                OldValue = new Text(oldValue),
                NewValue = new Text(newValue),
                IpAddress = new Text(ipAddress),
                UserAgent = new Text(userAgent),
                KeycloakSessionId = new Text(keycloakSessionId),
                CorellationId = new Text(corellationId)
            };
            return activityLog;
        }
    }

}
