using FluentValidation;

namespace Modulith.Commerce.Products.Application.ProductVariants.Commands.UpdateProductVariant
{
    public class UpdateProductVariantCommandValidator : AbstractValidator<UpdateProductVariantCommand>
    {
        public UpdateProductVariantCommandValidator()
        {
            RuleFor(x => x.VariantId).NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.Sku).NotEmpty().MaximumLength(64);
            RuleFor(x => x.Barcode).NotEmpty().MaximumLength(64);
            RuleFor(x => x.PriceAmount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PriceCurrency).NotEmpty().MaximumLength(3);
            RuleFor(x => x.StockQuantity).GreaterThanOrEqualTo(0);
            RuleFor(x => x.DiscountCurrency).NotEmpty().MaximumLength(3);

            RuleForEach(x => x.Images)
                .SetValidator(new UpdateProductVariantImageItemValidator())
                .When(x => x.Images.Count > 0);

            RuleForEach(x => x.Properties)
                .SetValidator(new UpdateProductVariantPropertyItemValidator())
                .When(x => x.Properties.Count > 0);
        }
    }

    public class UpdateProductVariantImageItemValidator : AbstractValidator<UpdateProductVariantImageItem>
    {
        public UpdateProductVariantImageItemValidator()
        {
            RuleFor(x => x.ImageUrl).NotEmpty().MaximumLength(2048);
        }
    }

    public class UpdateProductVariantPropertyItemValidator : AbstractValidator<UpdateProductVariantPropertyItem>
    {
        public UpdateProductVariantPropertyItemValidator()
        {
            RuleFor(x => x.CategoryPropertyId).NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.Value).NotEmpty().MaximumLength(50);
        }
    }
}
