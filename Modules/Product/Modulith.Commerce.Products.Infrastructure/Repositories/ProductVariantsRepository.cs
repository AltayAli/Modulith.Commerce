using Microsoft.AspNetCore.Http;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.Common.Infrastructure.Repositories;
using Modulith.Commerce.Products.Domain.ProductVariants;
using Modulith.Commerce.Products.Infrastructure.Data;

namespace Modulith.Commerce.Products.Infrastructure.Repositories
{
    public sealed class ProductVariantsRepository(
        ProductsDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<ProductVariant, ProductsDbContext>(dataContext, httpContextAccessor, dateTimeProvider), IProductVariantsRepository;
}
