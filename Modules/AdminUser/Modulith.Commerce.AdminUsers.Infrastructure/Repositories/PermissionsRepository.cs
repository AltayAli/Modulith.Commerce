using Microsoft.AspNetCore.Http;
using Modulith.Commerce.AdminUser.Domain.Permissions;
using Modulith.Commerce.AdminUsers.Infrastructure.Data;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.Common.Infrastructure.Repositories;

namespace Modulith.Commerce.AdminUsers.Infrastructure.Repositories
{
    public sealed class PermissionsRepository(
        AdminUsersDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<Permission, AdminUsersDbContext>(dataContext, httpContextAccessor, dateTimeProvider),
          IPermissionsRepository;
}
