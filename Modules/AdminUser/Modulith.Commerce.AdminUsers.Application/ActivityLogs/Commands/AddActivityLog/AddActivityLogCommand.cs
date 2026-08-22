using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.AdminUsers.Application.ActivityLogs.Commands.AddActivityLog
{
    public record AddActivityLogCommand : ICommand
    {
        public Guid UserId { get; set; }
        public string Action { get; set; }
        public string Resource { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string KeycloakSessionId { get; set; }
        public string CorellationId { get; set; }
    }
}
