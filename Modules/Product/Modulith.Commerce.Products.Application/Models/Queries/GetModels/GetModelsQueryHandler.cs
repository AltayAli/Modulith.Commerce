using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Models;

namespace Modulith.Commerce.Products.Application.Models.Queries.GetModels
{
    public class GetModelsQueryHandler(IModelsRepository modelsRepository)
        : IQueryHandler<GetModelsQuery, List<GetModelsQueryResponse>>
    {
        public async Task<Result<List<GetModelsQueryResponse>>> Handle(GetModelsQuery request, CancellationToken cancellationToken)
        {
            var models = await modelsRepository.SelectAsync(new FilteringOptions<Model>
            {
                Predicates = new List<System.Linq.Expressions.Expression<Func<Model, bool>>> {
                    m => m.BrandId == request.BrandId
                }
            }, cancellationToken);

            var results = models.Select(m => new GetModelsQueryResponse
            {
                Id = m.Id,
                Name = m.Name.Value,
                LastModifiedDate = m.ModifiedDate ?? m.AddedDate
            }).ToList();

            return Result.Success(results);
        }
    }
}
