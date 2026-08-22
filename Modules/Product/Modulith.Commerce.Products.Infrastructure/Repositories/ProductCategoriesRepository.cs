using Microsoft.AspNetCore.Http;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.Common.Infrastructure.Repositories;
using Modulith.Commerce.Products.Domain.ProductCategories;
using Modulith.Commerce.Products.Infrastructure.Data;

namespace Modulith.Commerce.Products.Infrastructure.Repositories
{
    public sealed class ProductCategoriesRepository(
        ProductsDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<ProductCategory, ProductsDbContext>(dataContext, httpContextAccessor, dateTimeProvider), IProductCategoriesRepository;
}
