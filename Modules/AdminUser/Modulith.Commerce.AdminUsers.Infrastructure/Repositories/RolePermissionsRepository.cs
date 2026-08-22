using Microsoft.AspNetCore.Http;
using Modulith.Commerce.AdminUser.Domain.RolePermissions;
using Modulith.Commerce.AdminUsers.Infrastructure.Data;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.Common.Infrastructure.Repositories;

namespace Modulith.Commerce.AdminUsers.Infrastructure.Repositories
{
    public sealed class RolePermissionsRepository(
        AdminUsersDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<RolePermission, AdminUsersDbContext>(dataContext, httpContextAccessor, dateTimeProvider),
          IRolePermissionsRepository;
}
