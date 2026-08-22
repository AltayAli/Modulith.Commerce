using Microsoft.AspNetCore.Http;
using Modulith.Commerce.AdminUser.Domain.Departments;
using Modulith.Commerce.AdminUsers.Infrastructure.Data;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.Common.Infrastructure.Repositories;

namespace Modulith.Commerce.AdminUsers.Infrastructure.Repositories
{
    public sealed class DepartmentsRepository(
        AdminUsersDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<Department, AdminUsersDbContext>(dataContext, httpContextAccessor, dateTimeProvider),
          IDepartmentsRepository;
}
