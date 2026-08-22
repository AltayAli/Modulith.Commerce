using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Products;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Application.Products.Queries.GetProducts
{
    public class GetProductsQueryHandler(IProductsRepository productsRepository)
        : IQueryHandler<GetProductsQuery, List<GetProductsItemResponse>>
    {
        public async Task<Result<List<GetProductsItemResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var predicates = new List<Expression<Func<Product, bool>>>();

            if (!string.IsNullOrWhiteSpace(request.Key))
                predicates.Add(p => p.Name.Value.Contains(request.Key));

            var query = await productsRepository.SelectAsync(new FilteringOptions<Product>
            {
                Predicates = predicates,
                Relations = new List<string> { "Model", "Variants" }
            }, cancellationToken);

            var response = query.Select(p => new GetProductsItemResponse
            {
                Id = p.Id,
                Name = p.Name.Value,
                Description = p.Description,
                Slug = p.Slug.Value,
                ShortDescription = p.ShortDescription,
                ModelName = p.Model != null ? p.Model.Name.Value : null,
                Status = p.Status.ToString(),
                IsFeatured = p.IsFeatured,
                TaxClass = p.TaxClass,
                AvgRating = p.AvgRating,
                ReviewCount = p.ReviewCount,
                PublishedAt = p.PublishedAt,
                VariantsCount = p.Variants != null ? p.Variants.Count : 0,
                LastModifiedDate = p.ModifiedDate ?? p.AddedDate
            }).ToList();

            return Result.Success(response);
        }
    }
}
