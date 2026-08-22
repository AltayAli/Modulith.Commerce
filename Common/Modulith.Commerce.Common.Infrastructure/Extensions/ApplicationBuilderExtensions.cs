using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Modulith.Commerce.Common.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplyModuleMigrations<T_DbContext>(this IApplicationBuilder app) where T_DbContext : DbContext
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<T_DbContext>();
            dbContext.Database.Migrate();
        }
    }
}
