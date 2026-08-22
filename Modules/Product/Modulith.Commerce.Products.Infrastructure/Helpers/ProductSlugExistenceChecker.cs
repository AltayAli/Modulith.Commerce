using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Application.Products;
using Modulith.Commerce.Products.Domain.Products;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Infrastructure.Helpers
{
    public class ProductSlugExistenceChecker(IProductsRepository productsRepository) : IProductSlugExistenceChecker
    {
        public async Task<bool> ExistsAsync(string slug, CancellationToken cancellationToken = default)
        {
            string normalizedSlug = slug.Trim().ToLower();
            bool slugExists = await productsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Product>
            {
                Predicates = new List<Expression<Func<Product, bool>>> {
                    p => p.Slug.Value.ToLower() == normalizedSlug
                }
            }, cancellationToken) is not null;

            return slugExists;
        }
    }
}
