using Microsoft.Extensions.Logging;
using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.ActivityLogs;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.ActivityLogs.Commands.AddActivityLog
{
    public class AddActivityLogCommandHandler(
                IActivityLogsRepository _repo,
                IUnitOfWork unitOfWork,
                ILogger<AddActivityLogCommandHandler> _logger) : ICommandHandler<AddActivityLogCommand>
    {
        public async Task<Result> Handle(AddActivityLogCommand request, CancellationToken cancellationToken)
        {
            var record = ActivityLog.Create(
                request.UserId,
                request.Action,
                request.Resource,
                request.OldValue,
                request.NewValue,
                request.IpAddress,
                request.UserAgent,
                request.KeycloakSessionId,
                request.CorellationId
            );

            await _repo.InsertAsync(record, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Activity log created for user {UserId} with action {Action} on resource {Resource}", request.UserId, request.Action, request.Resource);
            return Result.Success(record);
        }
    }
}
