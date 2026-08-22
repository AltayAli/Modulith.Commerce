using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Brands;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Application.Brands.Queries.GetBrand
{
    public class GetBrandQueryHandler(IBrandsRepository brandsRepository)
                        : IQueryHandler<GetBrandQuery, GetBrandResponse>
    {
        public async Task<Result<GetBrandResponse>> Handle(GetBrandQuery request, CancellationToken cancellationToken)
        {
            var brand = await brandsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Brand>
            {
                Predicates = new List<Expression<Func<Brand, bool>>>
                {
                    m => m.Id == request.Id,
                },
                Relations = new List<string> { "Models" }
            });

            if (brand == null)
            {
                return Result.Failure<GetBrandResponse>(value: null, BrandErrors.NotFound);
            }

            return Result.Success(new GetBrandResponse
            {
                Id = brand.Id,
                Name = brand.Name.Value,
                LastModifiedDate = brand.ModifiedDate ?? brand.AddedDate,
                ModelsCount = brand.Models.Count
            });
        }
    }
}
