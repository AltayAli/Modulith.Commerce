using FluentValidation;
using Modulith.Commerce.Products.Domain.CategoryProperties;
using Modulith.Commerce.Products.Domain.CategoryPropertyValues;

namespace Modulith.Commerce.Products.Application.CategoryProperties.Commands.UpdateCategoryProperty
{
    public class UpdateCategoryPropertyCommandValidator : AbstractValidator<UpdateCategoryPropertyCommand>
    {
        public UpdateCategoryPropertyCommandValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithErrorCode(CategoryPropertyErrors.NullValue.Code)
                .NotEqual(Guid.Empty)
                .WithErrorCode(CategoryPropertyErrors.NullValue.Code);

            RuleFor(x => x.Items)
                .NotNull()
                .WithMessage(CategoryPropertyErrors.NullValue.Code)
                .NotEmpty()
                .WithMessage(CategoryPropertyErrors.EmptyItems.Code);

            RuleForEach(x => x.Items)
                .SetValidator(new UpdateCategoryPropertyCommandItemValidator());
        }
    }

    public class UpdateCategoryPropertyCommandItemValidator : AbstractValidator<UpdateCategoryPropertyCommandItem>
    {
        public UpdateCategoryPropertyCommandItemValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode(CategoryPropertyErrors.NullValue.Code)
                .MaximumLength(100)
                .WithErrorCode(CategoryPropertyErrors.MaxLenght.Code);

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage(CategoryPropertyErrors.WrongType.Code);

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0)
                .WithMessage(CategoryPropertyErrors.WrongDisplayOrder.Code);

            RuleForEach(x => x.Values)
                .SetValidator(new UpdateCategoryPropertyValueItemValidator());
        }
    }

    public class UpdateCategoryPropertyValueItemValidator : AbstractValidator<UpdateCategoryPropertyValueItem>
    {
        public UpdateCategoryPropertyValueItemValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty()
                .WithErrorCode(CategoryPropertyValueErrors.NullValue.Code)
                .MaximumLength(100)
                .WithErrorCode(CategoryPropertyValueErrors.MaxLenght.Code);
        }
    }
}
