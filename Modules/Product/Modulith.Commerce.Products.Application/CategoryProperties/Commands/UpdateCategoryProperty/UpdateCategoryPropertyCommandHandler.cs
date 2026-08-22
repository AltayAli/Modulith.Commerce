using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Application.Exceptions;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.CategoryProperties;
using Modulith.Commerce.Products.Domain.CategoryPropertyValues;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Application.CategoryProperties.Commands.UpdateCategoryProperty
{
    public class UpdateCategoryPropertyCommandHandler
            (ICategoryPropertiesRepository propertiesRepository,
            ICategoryPropertyValuesRepository valuesRepository,
            IUnitOfWork unitOfWork) : ICommandHandler<UpdateCategoryPropertyCommand, List<Guid>>
    {
        public async Task<Result<List<Guid>>> Handle(UpdateCategoryPropertyCommand request, CancellationToken cancellationToken)
        {
            var properties = await propertiesRepository.SelectAsync(new FilteringOptions<CategoryProperty>
            {
                Predicates = new List<Expression<Func<CategoryProperty, bool>>>
                {
                    prop => prop.CategoryId == request.CategoryId
                },
            }, cancellationToken);

            var propertyIds = new List<Guid>();

            foreach (var item in request.Items)
            {
                Guid propertyId;

                if (item.Id is null)
                {
                    propertyId = await InsertPropertyAsync(request.CategoryId, item, cancellationToken);
                }
                else
                {
                    var property = properties.FirstOrDefault(p => p.Id == item.Id);

                    if (property is null)
                    {
                        return Result.Failure<List<Guid>>(propertyIds, CategoryPropertyErrors.NotFound);
                    }

                    await UpdatePropertyAsync(request.CategoryId, property, item, cancellationToken);
                    propertyId = property.Id;
                }

                propertyIds.Add(propertyId);

                await SyncPropertyValuesAsync(propertyId, item.Values, cancellationToken);
            }

            var removedProperties = properties.Where(p => !request.Items.Any(i => i.Id == p.Id)).ToList();

            if (removedProperties.Any())
            {
                await DeletePropertyAsync(removedProperties, cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(propertyIds);
        }

        private async Task<Guid> InsertPropertyAsync(Guid categoryId, UpdateCategoryPropertyCommandItem item, CancellationToken cancellationToken)
        {
            bool propertyAlreadyExists = await DoesCategoryPropertyExistAsync(categoryId, item.Name, item.Type);

            if (propertyAlreadyExists)
            {
                throw new UpdateCategoryPropertyAlreadyExistsException();
            }

            var property = CategoryProperty.Create(categoryId, item.Name, item.Type, item.AddToFilter, item.IsRequired, item.DisplayOrder);

            await propertiesRepository.InsertAsync(property, cancellationToken);

            return property.Id;
        }

        private async Task<bool> DoesCategoryPropertyExistAsync(Guid categoryId, string name, CategoryPropertyType type)
        {
            string normalizedName = name.Trim().ToLower();

            bool propertyAlreadyExists = await propertiesRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<CategoryProperty>
            {
                Predicates = new List<Expression<Func<CategoryProperty, bool>>>
                {
                    prop => prop.CategoryId == categoryId &&
                            prop.Name.Value.ToLower() == normalizedName &&
                            prop.Type == type
                },
            }) is not null;
            return propertyAlreadyExists;
        }

        private async Task UpdatePropertyAsync(Guid categoryId, CategoryProperty property, UpdateCategoryPropertyCommandItem item, CancellationToken cancellationToken)
        {
            bool propertyAlreadyExists = await DoesCategoryPropertyExistAsync(categoryId, item.Name, item.Type);

            if (propertyAlreadyExists)
            {
                throw new UpdateCategoryPropertyAlreadyExistsException();
            }

            property.Update(item.Name, item.Type, item.AddToFilter, item.IsRequired, item.DisplayOrder);

            await propertiesRepository.UpdateAsync(property, cancellationToken);
        }

        private async Task DeletePropertyAsync(List<CategoryProperty> properties, CancellationToken cancellationToken)
        {
            foreach (var property in properties)
            {
                await propertiesRepository.DeleteAsync(property, cancellationToken);
            }
        }

        private async Task SyncPropertyValuesAsync(Guid propertyId, List<UpdateCategoryPropertyValueItem> items, CancellationToken cancellationToken)
        {
            var values = await valuesRepository.SelectAsync(new FilteringOptions<CategoryPropertyValue>
            {
                Predicates = new List<Expression<Func<CategoryPropertyValue, bool>>>
                {
                    x => x.CategoryPropertyId == propertyId
                },
                IsLoadingAsNoTracking = true
            }, cancellationToken);

            foreach (var item in items)
            {
                if (item.Id is not null)
                {
                    var value = values.FirstOrDefault(x => x.Id == item.Id);

                    if (value is not null)
                    {
                        value.Update(item.Value);
                        await valuesRepository.UpdateAsync(value, cancellationToken);
                    }
                }
                else if (!values.Any(x => x.Value.Trim().ToLower() == item.Value.Trim().ToLower()))
                {
                    var newValue = CategoryPropertyValue.Create(propertyId, item.Value);
                    await valuesRepository.InsertAsync(newValue, cancellationToken);
                }
            }

            var removedValues = values.Where(x => !items.Any(i => i.Id == x.Id)).ToList();

            foreach (var value in removedValues)
            {
                await valuesRepository.DeleteAsync(value, cancellationToken);
            }
        }
    }
}
