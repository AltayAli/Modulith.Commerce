using Microsoft.AspNetCore.Http;
using Modulith.Commerce.AdminUser.Domain.ActivityLogs;
using Modulith.Commerce.AdminUsers.Infrastructure.Data;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.Common.Infrastructure.Repositories;

namespace Modulith.Commerce.AdminUsers.Infrastructure.Repositories
{
    public sealed class ActivityLogsRepository(
        AdminUsersDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<ActivityLog, AdminUsersDbContext>(dataContext, httpContextAccessor, dateTimeProvider),
          IActivityLogsRepository;
}
