using Microsoft.AspNetCore.Http;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.Common.Infrastructure.Repositories;
using Modulith.Commerce.Products.Domain.Categories;
using Modulith.Commerce.Products.Domain.CategoryProperties;
using Modulith.Commerce.Products.Infrastructure.Data;

namespace Modulith.Commerce.Products.Infrastructure.Repositories
{
    public sealed class CategoryPropertiesRepository(
        ProductsDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<CategoryProperty, ProductsDbContext>(dataContext, httpContextAccessor, dateTimeProvider), ICategoryPropertiesRepository;
}
