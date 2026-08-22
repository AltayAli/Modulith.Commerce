using FluentValidation;
using Modulith.Commerce.Products.Domain.CategoryPropertyValues;

namespace Modulith.Commerce.Products.Application.CategoryPropertyValues.Commands
{
    public class AddCategoryPropertyValueCommandValidator : AbstractValidator<AddCategoryPropertyValueCommand>
    {
        public AddCategoryPropertyValueCommandValidator()
        {
            RuleForEach(x => x.Items)
                .NotEmpty()
                .WithErrorCode(CategoryPropertyValueErrors.NullValue.Code)
                .MaximumLength(50)
                .WithErrorCode(CategoryPropertyValueErrors.MaxLenght.Code);
        }
    }
}
