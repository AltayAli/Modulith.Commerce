using Microsoft.AspNetCore.Http;
using Modulith.Commerce.AdminUser.Domain.Roles;
using Modulith.Commerce.AdminUsers.Infrastructure.Data;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.Common.Infrastructure.Repositories;

namespace Modulith.Commerce.AdminUsers.Infrastructure.Repositories
{
    public sealed class RolesRepository(
        AdminUsersDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<Role, AdminUsersDbContext>(dataContext, httpContextAccessor, dateTimeProvider),
          IRolesRepository;
}
