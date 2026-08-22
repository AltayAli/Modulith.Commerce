using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.CategoryPropertyValues;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Application.CategoryPropertyValues.Commands
{
    public class AddCategoryPropertyValueCommandHandler
            (ICategoryPropertyValuesRepository _valuesRepository,
            IUnitOfWork unitOfWork)
        : ICommandHandler<AddCategoryPropertyValueCommand>
    {
        public async Task<Result> Handle(AddCategoryPropertyValueCommand request, CancellationToken cancellationToken)
        {
            var values = await _valuesRepository.SelectAsync(new FilteringOptions<CategoryPropertyValue>
            {
                Predicates = new List<Expression<Func<CategoryPropertyValue, bool>>>
                    {
                        x => x.CategoryPropertyId == request.PropertyId
                    },
                IsLoadingAsNoTracking = true
            });
            foreach (var item in request.Items)
            {
                if (!values.Any(x => x.Value.Trim().ToLower() == item.Normalize()))
                {
                    var value = CategoryPropertyValue.Create(request.PropertyId, item);

                    await _valuesRepository.InsertAsync(value, cancellationToken);
                }
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
