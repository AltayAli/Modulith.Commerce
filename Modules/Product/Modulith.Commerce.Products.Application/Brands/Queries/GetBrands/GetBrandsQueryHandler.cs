using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Brands;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Application.Brands.Queries.GetBrands
{
    internal class GetBrandsQueryHandler(IBrandsRepository brandsRepository)
        : IQueryHandler<GetBrandsQuery, List<GetBrandsResponse>>
    {
        public async Task<Result<List<GetBrandsResponse>>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await brandsRepository.SelectAsync(new FilteringOptions<Brand>
            {
                Predicates = new List<Expression<Func<Brand, bool>>>
                {
                    m => m.Name.Value.Contains(request.Key) || string.IsNullOrEmpty(request.Key)
                },
                Relations = new List<string> { "Models" }
            });

            var response = brands.Select(m => new GetBrandsResponse
            {
                Id = m.Id,
                Name = m.Name.Value,
                LastModifiedDate = m.ModifiedDate ?? m.AddedDate,
                ModelsCount = m.Models.Count
            }).ToList();

            return Result.Success(response);
        }
    }
}
