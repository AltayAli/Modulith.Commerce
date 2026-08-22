using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Products;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Application.Products.Queries.GetProduct
{
    public class GetProductQueryHandler(IProductsRepository productsRepository)
        : IQueryHandler<GetProductQuery, GetProductResponse>
    {
        public async Task<Result<GetProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await productsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Product>
            {
                Predicates = new List<Expression<Func<Product, bool>>>
                {
                    p => p.Id == request.Id
                },
                Relations = new List<string>
                {
                    "Model",
                    "ProductCategories.Category",
                    "Variants.Images",
                    "Variants.Properties.CategoryProperty"
                }
            }, cancellationToken);

            if (product is null)
                return Result.Failure<GetProductResponse>(null, ProductErrors.NotFound);

            var response = new GetProductResponse
            {
                Id = product.Id,
                Name = product.Name.Value,
                Description = product.Description,
                Slug = product.Slug.Value,
                ShortDescription = product.ShortDescription,
                ModelId = product.ModelId,
                ModelName = product.Model?.Name.Value,
                Status = product.Status.ToString(),
                IsFeatured = product.IsFeatured,
                TaxClass = product.TaxClass,
                Seo = product.Seo is null ? null : new GetProductSeoItem
                {
                    Title = product.Seo.Title,
                    Description = product.Seo.Description,
                    Keywords = product.Seo.Keywords,
                    OgImage = product.Seo.OgImage
                },
                AvgRating = product.AvgRating,
                ReviewCount = product.ReviewCount,
                PublishedAt = product.PublishedAt,
                LastModifiedDate = product.ModifiedDate ?? product.AddedDate,
                Categories = product.ProductCategories.Select(pc => new GetProductCategoryItem
                {
                    Id = pc.Category.Id,
                    Name = pc.Category.Name.Value
                }).ToList(),
                Variants = product.Variants.Select(v => new GetProductVariantItem
                {
                    Id = v.Id,
                    Sku = v.Sku.Value,
                    Barcode = v.Barcode.Value,
                    PriceAmount = v.Price.Amount,
                    PriceCurrency = v.Price.Currency.Code,
                    CostAmount = v.Cost?.Amount,
                    CostCurrency = v.Cost?.Currency.Code,
                    TaxRate = v.TaxRate,
                    StockQuantity = v.Stock.Quantity,
                    IsDefault = v.IsDefault,
                    IsActive = v.IsActive,
                    DiscountAmount = v.DiscountCount.Amount,
                    DiscountCurrency = v.DiscountCount.Currency.Code,
                    DiscountStartDate = v.DiscountStartDate,
                    DiscountEndDate = v.DiscountEndDate,
                    Images = v.Images.Select(i => new GetProductVariantImageItem
                    {
                        Id = i.Id,
                        ImageUrl = i.ImageUrl.Url,
                        IsMain = i.IsMain
                    }).ToList(),
                    Properties = v.Properties.Select(p => new GetProductVariantPropertyItem
                    {
                        Id = p.Id,
                        CategoryPropertyId = p.CategoryPropertyId,
                        CategoryPropertyName = p.CategoryProperty.Name.Value,
                        Value = p.Value
                    }).ToList()
                }).ToList()
            };

            return Result.Success(response);
        }
    }
}
