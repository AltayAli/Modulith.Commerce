using FluentValidation;

namespace Modulith.Commerce.Products.Application.ProductVariantProperties.Commands.AddProductVariantProperties
{
    public class AddProductVariantPropertiesCommandValidator : AbstractValidator<AddProductVariantPropertiesCommand>
    {
        public AddProductVariantPropertiesCommandValidator()
        {
            RuleFor(x => x.VariantId).NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.Properties).NotEmpty();
            RuleForEach(x => x.Properties).SetValidator(new AddProductVariantPropertyItemValidator());
        }
    }

    public class AddProductVariantPropertyItemValidator : AbstractValidator<AddProductVariantPropertyItem>
    {
        public AddProductVariantPropertyItemValidator()
        {
            RuleFor(x => x.CategoryPropertyId).NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.Value).NotEmpty().MaximumLength(50);
        }
    }
}
