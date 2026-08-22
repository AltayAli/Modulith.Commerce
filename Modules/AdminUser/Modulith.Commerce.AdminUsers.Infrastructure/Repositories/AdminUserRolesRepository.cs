using Microsoft.AspNetCore.Http;
using Modulith.Commerce.AdminUser.Domain.AdminUserRoles;
using Modulith.Commerce.AdminUsers.Infrastructure.Data;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.Common.Infrastructure.Repositories;

namespace Modulith.Commerce.AdminUsers.Infrastructure.Repositories
{
    public sealed class AdminUserRolesRepository(
        AdminUsersDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<AdminUserRole, AdminUsersDbContext>(dataContext, httpContextAccessor, dateTimeProvider),
          IAdminUserRolesRepository;
}
