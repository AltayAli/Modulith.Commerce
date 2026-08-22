using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Modulith.Commerce.Common.Infrastructure.Clock;

namespace Modulith.Commerce.Products.Infrastructure.Data
{
    public class ProductsDbContextFactory : IDesignTimeDbContextFactory<ProductsDbContext>
    {
        public ProductsDbContext CreateDbContext(string[] args)
        {
            string password = Environment.GetEnvironmentVariable("SA_PASSWORD")
                ?? throw new InvalidOperationException(
                    "SA_PASSWORD environment variable is required for EF Core design-time operations (set it in .env or export it before running dotnet ef).");

            var optionsBuilder = new DbContextOptionsBuilder<ProductsDbContext>();
            optionsBuilder.UseSqlServer(
                $"Server=localhost,1433;Database=ModulithCommerceDB;User Id=sa;Password={password};TrustServerCertificate=True;");

            return new ProductsDbContext(new DateTimeProvider(), optionsBuilder.Options);
        }
    }
}
