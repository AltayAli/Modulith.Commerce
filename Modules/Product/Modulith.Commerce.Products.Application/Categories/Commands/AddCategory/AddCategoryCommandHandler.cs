using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Categories;

namespace Modulith.Commerce.Products.Application.Categories.Commands.AddCategory
{
    public class AddCategoryCommandHandler
        (ICategoriesRepository categoriesRepository,
            IUnitOfWork unitOfWork)
        : ICommandHandler<AddCategoryCommand>
    {
        public async Task<Result> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.ParentId is not null)
            {
                bool parentExists = await categoriesRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Category>
                {
                    Predicates = new List<System.Linq.Expressions.Expression<Func<Category, bool>>> {
                        m => m.Id == request.ParentId
                    }
                }, cancellationToken) is not null;

                if (!parentExists)
                {
                    return Result.Failure(CategoryErrors.ParentCategoryNotFound);
                }
            }

            string normalizedName = request.Name.Trim().ToLower();
            bool categoryExists = await categoriesRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Category>
            {
                Predicates = new List<System.Linq.Expressions.Expression<Func<Category, bool>>> {
                    m => m.Name.Value.ToLower() == normalizedName && m.ParentId == request.ParentId
                }
            }, cancellationToken) is not null;

            if (categoryExists)
            {
                return Result.Failure(CategoryErrors.AlreadyExists);
            }



            var category = Category.Create(request.Name, request.Icon, request.ParentId);

            await categoriesRepository.InsertAsync(category, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(category);
        }
    }
}
