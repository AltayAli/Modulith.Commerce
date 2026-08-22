using FluentValidation;
using Modulith.Commerce.Products.Domain.CategoryProperties;

namespace Modulith.Commerce.Products.Application.CategoryProperties.Commands.AddCategoryProperty
{
    public class AddCategoryPropertyCommandValidator : AbstractValidator<AddCategoryPropertyCommand>
    {
        public AddCategoryPropertyCommandValidator()
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
                .SetValidator(new AddCategoryPropertyCommandItemValidator());
        }
    }

    public class AddCategoryPropertyCommandItemValidator : AbstractValidator<AddCategoryPropertyCommandItem>
    {
        public AddCategoryPropertyCommandItemValidator()
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
        }
    }
}
