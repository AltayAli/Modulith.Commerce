using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Categories;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Application.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler
        (ICategoriesRepository categoriesRepository)
        : IQueryHandler<GetCategoriesQuery, List<GetCategoriesItemResponse>>
    {
        public async Task<Result<List<GetCategoriesItemResponse>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await categoriesRepository.SelectAsync(new FilteringOptions<Category>
            {
                Predicates = new List<Expression<Func<Category, bool>>>
                {
                    m => string.IsNullOrEmpty(request.Key) || m.Name.Value.Contains(request.Key) ,
                    m => request.ParentId == null || m.ParentId == request.ParentId
                },
                Relations = new List<string> { "Children" }
            }, cancellationToken);

            var response = categories.Select(m => new GetCategoriesItemResponse
            {
                Id = m.Id,
                Name = m.Name.Value,
                LastModifiedDate = m.ModifiedDate ?? m.AddedDate,
                SubCategoriesCount = m.Children.Count,
                Icon = m.Icon != null ? m.Icon.Value : string.Empty
            }).ToList();

            return Result.Success(response);
        }
    }
}
