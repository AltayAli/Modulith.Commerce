using Microsoft.AspNetCore.Http;
using Modulith.Commerce.AdminUser.Domain.AdminUsers;
using Modulith.Commerce.AdminUsers.Infrastructure.Data;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.Common.Infrastructure.Repositories;

namespace Modulith.Commerce.AdminUsers.Infrastructure.Repositories
{
    public sealed class AdminUsersRepository(
        AdminUsersDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser, AdminUsersDbContext>(dataContext, httpContextAccessor, dateTimeProvider),
          IAdminUsersRepository;
}
