using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modulith.Commerce.Common.Infrastructure.Extensions;
using Modulith.Commerce.Products.Application.Brands;
using Modulith.Commerce.Products.Application.Models;
using Modulith.Commerce.Products.Application.Products;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Infrastructure.Data;
using Modulith.Commerce.Products.Infrastructure.Helpers;
using Modulith.Commerce.Products.Presentation.Brands;
using Modulith.Commerce.Products.Presentation.Categories;
using Modulith.Commerce.Products.Presentation.Categories.Mapping;
using Modulith.Commerce.Products.Presentation.CategoryProperties;
using Modulith.Commerce.Products.Presentation.Models;
using Modulith.Commerce.Products.Presentation.Products;

namespace Modulith.Commerce.Products.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddProductModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBrandExistenceChecker, BrandExistenceChecker>();
            services.AddScoped<IModelExistenceChecker, ModelExistenceChecker>();
            services.AddScoped<IProductSlugExistenceChecker, ProductSlugExistenceChecker>();

            AddInfrastructure(services, configuration);

            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(CategoryMappingProfile).Assembly));
        }
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddDbContext<ProductsDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ProductsDbContext>());

            services.AddRepositories<ProductsDbContext>();
        }

        public static IEndpointRouteBuilder MapProductModuleEndpoints(this IEndpointRouteBuilder versionedGroup)
        {
            versionedGroup.MapCategoryEndpoints();
            versionedGroup.MapCategoryPropertyEndpoints();
            versionedGroup.MapBrandEndpoints();
            versionedGroup.MapModelEndpoints();
            versionedGroup.MapProductEndpoints();
            versionedGroup.MapProductVariantEndpoints();

            return versionedGroup;
        }
    }
}
