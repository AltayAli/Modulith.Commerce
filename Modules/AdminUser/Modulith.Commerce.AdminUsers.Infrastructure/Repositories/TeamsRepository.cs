using Microsoft.AspNetCore.Http;
using Modulith.Commerce.AdminUser.Domain.Teams;
using Modulith.Commerce.AdminUsers.Infrastructure.Data;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.Common.Infrastructure.Repositories;

namespace Modulith.Commerce.AdminUsers.Infrastructure.Repositories
{
    public sealed class TeamsRepository(
        AdminUsersDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<Team, AdminUsersDbContext>(dataContext, httpContextAccessor, dateTimeProvider),
          ITeamsRepository;
}
