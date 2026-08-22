using AutoMapper;
using Modulith.Commerce.Products.Application.Products.Commands.CreateProduct;
using Modulith.Commerce.Products.Application.Products.Commands.UpdateProduct;
using Modulith.Commerce.Products.Application.Products.Queries.GetProduct;
using Modulith.Commerce.Products.Application.Products.Queries.GetProducts;
using Modulith.Commerce.Products.Application.ProductVariants.Commands.AddProductVariant;
using Modulith.Commerce.Products.Application.ProductVariants.Commands.UpdateProductVariant;
using Modulith.Commerce.Products.Presentation.Products.DTOs;

namespace Modulith.Commerce.Products.Presentation.Products.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {

            CreateMap<AddProductRequestDto, CreateProductCommand>();
            CreateMap<UpdateProductRequestDto, UpdateProductCommand>();
            CreateMap<SeoRequestDto, SeoRequest>();

            CreateMap<AddProductVariantRequestDto, AddProductVariantCommand>();
            CreateMap<AddProductVariantImageItemDto, AddProductVariantImageItem>();
            CreateMap<AddProductVariantPropertyItemDto, AddProductVariantPropertyItem>();

            CreateMap<UpdateProductVariantRequestDto, UpdateProductVariantCommand>();
            CreateMap<UpdateProductVariantImageItemDto, UpdateProductVariantImageItem>();
            CreateMap<UpdateProductVariantPropertyItemDto, UpdateProductVariantPropertyItem>();

            CreateMap<GetProductsItemResponse, ProductListResponseDto>();
            CreateMap<GetProductResponse, ProductDetailResponseDto>();
            CreateMap<GetProductSeoItem, ProductSeoItemDto>();
            CreateMap<GetProductCategoryItem, ProductCategoryItemDto>();
            CreateMap<GetProductVariantItem, ProductVariantItemDto>();
            CreateMap<GetProductVariantImageItem, ProductVariantImageItemDto>();
            CreateMap<GetProductVariantPropertyItem, ProductVariantPropertyItemDto>();
        }
    }
}
