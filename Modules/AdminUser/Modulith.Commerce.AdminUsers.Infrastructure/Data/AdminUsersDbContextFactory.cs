using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Modulith.Commerce.Common.Infrastructure.Clock;

namespace Modulith.Commerce.AdminUsers.Infrastructure.Data
{
    public class AdminUsersDbContextFactory : IDesignTimeDbContextFactory<AdminUsersDbContext>
    {
        public AdminUsersDbContext CreateDbContext(string[] args)
        {
            string password = Environment.GetEnvironmentVariable("SA_PASSWORD")
                ?? throw new InvalidOperationException(
                    "SA_PASSWORD environment variable is required for EF Core design-time operations (set it in .env or export it before running dotnet ef).");

            var optionsBuilder = new DbContextOptionsBuilder<AdminUsersDbContext>();
            optionsBuilder.UseSqlServer(
                $"Server=localhost,1433;Database=ModulithCommerceAdminUsersDB;User Id=sa;Password={password};TrustServerCertificate=True;");

            return new AdminUsersDbContext(new DateTimeProvider(), new NoOpPublisher(), optionsBuilder.Options);
        }

        private sealed class NoOpPublisher : IPublisher
        {
            public Task Publish(object notification, CancellationToken cancellationToken = default) => Task.CompletedTask;

            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
                where TNotification : INotification => Task.CompletedTask;
        }
    }
}
