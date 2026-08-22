using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.CategoryProperties;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Application.CategoryProperties.Commands.AddCategoryProperty
{
    public class AddCategoryPropertyCommandHandler
            (ICategoryPropertiesRepository propertiesRepository,
            IUnitOfWork unitOfWork)
        : ICommandHandler<AddCategoryPropertyCommand, List<Guid>>
    {
        public async Task<Result<List<Guid>>> Handle(AddCategoryPropertyCommand request, CancellationToken cancellationToken)
        {
            var properties = await propertiesRepository.SelectAsync(new FilteringOptions<CategoryProperty>
            {
                Predicates = new List<Expression<Func<CategoryProperty, bool>>>
                {
                    prop => prop.CategoryId == request.CategoryId
                },
            });

            var createdIds = new List<Guid>();

            foreach (var item in request.Items)
            {
                string normalizedName = item.Name.Trim().ToLower();

                bool propertyAlreadyExists = properties.Any(prop => prop.Name.Value.ToLower() == normalizedName && prop.Type == item.Type);

                if (propertyAlreadyExists)
                {
                    return Result.Failure<List<Guid>>(createdIds, CategoryPropertyErrors.AlreadyExists);
                }

                var categoryProperty = CategoryProperty.Create(
                    request.CategoryId,
                    item.Name,
                    item.Type,
                    item.AddToFilter,
                    item.IsRequired,
                    item.DisplayOrder
                );

                await propertiesRepository.InsertAsync(categoryProperty, cancellationToken);
                createdIds.Add(categoryProperty.Id);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(createdIds);
        }
    }
}
