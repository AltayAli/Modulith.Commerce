using Microsoft.AspNetCore.Http;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.Common.Infrastructure.Repositories;
using Modulith.Commerce.Products.Domain.CategoryPropertyValues;
using Modulith.Commerce.Products.Infrastructure.Data;

namespace Modulith.Commerce.Products.Infrastructure.Repositories
{
    public sealed class CategoryPropertyValuesRepository(
        ProductsDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<CategoryPropertyValue, ProductsDbContext>(dataContext, httpContextAccessor, dateTimeProvider), ICategoryPropertyValuesRepository;
}
