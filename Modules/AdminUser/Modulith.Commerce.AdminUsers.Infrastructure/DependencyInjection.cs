using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUsers.Application.AdminUsers;
using Modulith.Commerce.AdminUsers.Application.Roles;
using Modulith.Commerce.AdminUsers.Infrastructure.Bootstrap;
using Modulith.Commerce.AdminUsers.Infrastructure.Data;
using Modulith.Commerce.AdminUsers.Presentation.AdminUsers;
using Modulith.Commerce.AdminUsers.Presentation.AdminUserRoles;
using Modulith.Commerce.AdminUsers.Presentation.Departments;
using Modulith.Commerce.AdminUsers.Presentation.Roles;
using Modulith.Commerce.AdminUsers.Presentation.Roles.Mapping;
using Modulith.Commerce.AdminUsers.Presentation.RolePermissions;
using Modulith.Commerce.AdminUsers.Presentation.Teams;
using Modulith.Commerce.Common.Infrastructure.Extensions;
using Modulith.Commerce.AdminUsers.Presentation.Auth;

namespace Modulith.Commerce.AdminUsers.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddAdminUserModule(this IServiceCollection services, IConfiguration configuration)
        {
            AddInfrastructure(services, configuration);
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(RoleMappingProfile).Assembly));
        }

        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddDbContext<AdminUsersDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AdminUsersDbContext>());
            services.AddRepositories<AdminUsersDbContext>();
            services.AddScoped<IAdminUserKeycloakSyncService, AdminUserKeycloakSyncService>();
            services.AddScoped<IRoleKeycloakSyncService, RoleKeycloakSyncService>();

            services.Configure<BootstrapAdministratorOptions>(configuration.GetSection(BootstrapAdministratorOptions.SectionName));
            services.AddHostedService<BootstrapAdministratorHostedService>();
        }

        public static IEndpointRouteBuilder MapAdminUserModuleEndpoints(this IEndpointRouteBuilder versionedGroup)
        {
            versionedGroup.MapAdminUserEndpoints();
            versionedGroup.MapAdminUserRoleEndpoints();
            versionedGroup.MapRoleEndpoints();
            versionedGroup.MapDepartmentEndpoints();
            versionedGroup.MapTeamEndpoints();
            versionedGroup.MapRolePermissionEndpoints();
            versionedGroup.MapAdminAuthEndpoints();
            return versionedGroup;
        }
    }
}
