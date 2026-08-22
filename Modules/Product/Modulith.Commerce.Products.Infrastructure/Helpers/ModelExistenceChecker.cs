using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Application.Models;
using Modulith.Commerce.Products.Domain.Models;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Infrastructure.Helpers
{
    public class ModelExistenceChecker(IModelsRepository modelsRepository) : IModelExistenceChecker
    {
        public async Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default)
        {
            string normalizedName = name.Trim().ToLower();
            bool modelExists = await modelsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Model>
            {
                Predicates = new List<Expression<Func<Model, bool>>> {
                    m => m.Name.Value.ToLower() == normalizedName
                }
            }, cancellationToken) is not null;

            return modelExists;
        }
    }
}
