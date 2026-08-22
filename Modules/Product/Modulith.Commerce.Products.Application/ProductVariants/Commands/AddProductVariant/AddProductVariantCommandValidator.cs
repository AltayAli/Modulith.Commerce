using FluentValidation;

namespace Modulith.Commerce.Products.Application.ProductVariants.Commands.AddProductVariant
{
    public class AddProductVariantCommandValidator : AbstractValidator<AddProductVariantCommand>
    {
        public AddProductVariantCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.Sku).NotEmpty().MaximumLength(64);
            RuleFor(x => x.Barcode).NotEmpty().MaximumLength(64);
            RuleFor(x => x.PriceAmount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PriceCurrency).NotEmpty().MaximumLength(3);
            RuleFor(x => x.StockQuantity).GreaterThanOrEqualTo(0);
            RuleFor(x => x.DiscountCurrency).NotEmpty().MaximumLength(3);

            RuleForEach(x => x.Images)
                .SetValidator(new AddProductVariantImageItemValidator())
                .When(x => x.Images.Count > 0);

            RuleForEach(x => x.Properties)
                .SetValidator(new AddProductVariantPropertyItemValidator())
                .When(x => x.Properties.Count > 0);
        }
    }

    public class AddProductVariantImageItemValidator : AbstractValidator<AddProductVariantImageItem>
    {
        public AddProductVariantImageItemValidator()
        {
            RuleFor(x => x.ImageUrl).NotEmpty().MaximumLength(2048);
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
