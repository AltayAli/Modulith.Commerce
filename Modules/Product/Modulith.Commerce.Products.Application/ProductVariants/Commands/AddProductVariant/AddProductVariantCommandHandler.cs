using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Common.Domain.ValueObjects;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Products;
using Modulith.Commerce.Products.Domain.ProductVariantImages;
using Modulith.Commerce.Products.Domain.ProductVariantProperties;
using Modulith.Commerce.Products.Domain.ProductVariants;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Application.ProductVariants.Commands.AddProductVariant
{
    public class AddProductVariantCommandHandler(
        IProductsRepository productsRepository,
        IProductVariantsRepository productVariantsRepository,
        IProductVariantImagesRepository productVariantImagesRepository,
        IProductVariantPropertiesRepository productVariantPropertiesRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<AddProductVariantCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(AddProductVariantCommand request, CancellationToken cancellationToken)
        {
            bool productExists = await productsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Product>
            {
                Predicates = new List<Expression<Func<Product, bool>>>
                {
                    p => p.Id == request.ProductId
                }
            }) is not null;

            if (!productExists)
                return Result.Failure<Guid>(default, ProductErrors.NotFound);

            var price = new Money(request.PriceAmount, Currency.GetFromCode(request.PriceCurrency));

            Money? cost = request.CostAmount.HasValue && request.CostCurrency is not null
                ? new Money(request.CostAmount.Value, Currency.GetFromCode(request.CostCurrency))
                : null;

            var discount = new Money(request.DiscountAmount, Currency.GetFromCode(request.DiscountCurrency));

            var variant = ProductVariant.Create(
                request.ProductId,
                request.Sku,
                request.Barcode,
                price,
                cost,
                request.TaxRate,
                request.StockQuantity,
                request.IsDefault,
                request.IsActive,
                discount,
                request.DiscountStartDate,
                request.DiscountEndDate);

            await productVariantsRepository.InsertAsync(variant, cancellationToken);

            foreach (var item in request.Images)
            {
                var image = ProductVariantImage.Create(variant.Id, item.ImageUrl, item.IsMain);
                await productVariantImagesRepository.InsertAsync(image, cancellationToken);
            }

            foreach (var item in request.Properties)
            {
                var property = ProductVariantProperty.Create(variant.Id, item.CategoryPropertyId, item.Value);
                await productVariantPropertiesRepository.InsertAsync(property, cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<Guid>.Success(variant.Id);
        }
    }
}
