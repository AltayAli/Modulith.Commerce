using FluentValidation;
using Modulith.Commerce.Products.Domain.Categories;

namespace Modulith.Commerce.Products.Application.Categories.Commands.AddCategory
{
    public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(CategoryErrors.NullValue.Code)
                .MaximumLength(100)
                .WithMessage(CategoryErrors.MaxLenght.Code);
            RuleFor(x => x.Icon)
               .MaximumLength(200)
               .WithMessage(CategoryErrors.MaxLenght.Code);
        }
    }
}
