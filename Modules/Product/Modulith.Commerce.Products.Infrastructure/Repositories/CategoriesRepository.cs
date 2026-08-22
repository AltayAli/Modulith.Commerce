using Microsoft.AspNetCore.Http;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.Common.Infrastructure.Repositories;
using Modulith.Commerce.Products.Domain.Categories;
using Modulith.Commerce.Products.Infrastructure.Data;

namespace Modulith.Commerce.Products.Infrastructure.Repositories
{
    public sealed class CategoriesRepository(
        ProductsDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<Category, ProductsDbContext>(dataContext, httpContextAccessor, dateTimeProvider), ICategoriesRepository;
}
