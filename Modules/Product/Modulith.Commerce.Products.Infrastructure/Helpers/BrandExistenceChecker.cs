using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Application.Brands;
using Modulith.Commerce.Products.Domain.Brands;

namespace Modulith.Commerce.Products.Infrastructure.Helpers
{
    public class BrandExistenceChecker(IBrandsRepository brandsRepository) : IBrandExistenceChecker
    {
        public async Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default)
        {
            string normalizedName = name.Trim().ToLower();
            bool brandExists = await brandsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Brand>
            {
                Predicates = new List<System.Linq.Expressions.Expression<Func<Brand, bool>>> {
                    m => m.Name.Value.ToLower() == normalizedName
                }
            }, cancellationToken) is not null;

            return brandExists;
        }
    }
}
