using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Categories;
using Modulith.Commerce.Products.Domain.Brands;

namespace Modulith.Commerce.Products.Application.Categories.Commands.RemoveCategory
{
    public class RemoveCatetegoryCommandHandler
        (IUnitOfWork unitOfWork,
        ICategoriesRepository categoriesRepository)
        : ICommandHandler<RemoveCategoryCommand>
    {
        public async Task<Result> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await categoriesRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Category>
            {
                Predicates = new List<System.Linq.Expressions.Expression<Func<Category, bool>>> {
                    m => m.Id == request.Id
                },
                IsLoadingAsNoTracking = false
            }, cancellationToken);

            if (category is null)
            {
                return Result.Failure(BrandErrors.NotFound);
            }

            category.Remove();

            await categoriesRepository.DeleteAsync(category, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
