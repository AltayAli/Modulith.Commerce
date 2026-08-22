using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Commerce.Products.Application.ProductVariantImages.Commands.AddProductVariantImages;
using Modulith.Commerce.Products.Application.ProductVariantImages.Commands.RemoveProductVariantImage;
using Modulith.Commerce.Products.Application.ProductVariantProperties.Commands.AddProductVariantProperties;
using Modulith.Commerce.Products.Application.ProductVariantProperties.Commands.RemoveProductVariantProperty;
using Modulith.Commerce.Products.Application.ProductVariants.Commands.AddProductVariant;
using Modulith.Commerce.Products.Application.ProductVariants.Commands.RemoveProductVariant;
using Modulith.Commerce.Products.Application.ProductVariants.Commands.UpdateProductVariant;
using Modulith.Commerce.Products.Presentation.Authorization;
using Modulith.Commerce.Products.Presentation.Products.DTOs;
using AppendImageItem = Modulith.Commerce.Products.Application.ProductVariantImages.Commands.AddProductVariantImages.AddProductVariantImageItem;
using AppendPropertyItem = Modulith.Commerce.Products.Application.ProductVariantProperties.Commands.AddProductVariantProperties.AddProductVariantPropertyItem;

namespace Modulith.Commerce.Products.Presentation.Products
{
    public static class ProductVariantEndpoints
    {
        public static IEndpointRouteBuilder MapProductVariantEndpoints(this IEndpointRouteBuilder app)
        {
            var variants = app.MapGroup("products/{productId:guid}/variants").WithTags("ProductVariants");

            variants.MapPost("", AddVariant).RequireAuthorization(ProductPolicies.ProductVariantWrite);
            variants.MapPut("{variantId:guid}", UpdateVariant).RequireAuthorization(ProductPolicies.ProductVariantWrite);
            variants.MapDelete("{variantId:guid}", RemoveVariant).RequireAuthorization(ProductPolicies.ProductVariantDelete);

            var images = app.MapGroup("variants/{variantId:guid}/images").WithTags("ProductVariants");

            images.MapPost("", AddImages).RequireAuthorization(ProductPolicies.ProductVariantWrite);
            images.MapDelete("{imageId:guid}", RemoveImage).RequireAuthorization(ProductPolicies.ProductVariantDelete);

            var properties = app.MapGroup("variants/{variantId:guid}/properties").WithTags("ProductVariants");

            properties.MapPost("", AddProperties).RequireAuthorization(ProductPolicies.ProductVariantWrite);
            properties.MapDelete("{propertyId:guid}", RemoveProperty).RequireAuthorization(ProductPolicies.ProductVariantDelete);

            return app;
        }

        private static async Task<IResult> AddVariant(
            Guid productId,
            ISender sender,
            IMapper mapper,
            AddProductVariantRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(
                mapper.Map<AddProductVariantCommand>(request) with { ProductId = productId },
                cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Created($"variants/{result.Value}/images", result.Value);
        }

        private static async Task<IResult> UpdateVariant(
            Guid variantId,
            ISender sender,
            IMapper mapper,
            UpdateProductVariantRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(
                mapper.Map<UpdateProductVariantCommand>(request) with { VariantId = variantId },
                cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> RemoveVariant(
            Guid variantId,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new RemoveProductVariantCommand { Id = variantId }, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> AddImages(
            Guid variantId,
            ISender sender,
            AddProductVariantImagesRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new AddProductVariantImagesCommand
            {
                VariantId = variantId,
                Images = request.Images.Select(i => new AppendImageItem
                {
                    ImageUrl = i.ImageUrl,
                    IsMain = i.IsMain
                }).ToList()
            }, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> RemoveImage(
            Guid imageId,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new RemoveProductVariantImageCommand { Id = imageId }, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> AddProperties(
            Guid variantId,
            ISender sender,
            AddProductVariantPropertiesRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new AddProductVariantPropertiesCommand
            {
                VariantId = variantId,
                Properties = request.Properties.Select(p => new AppendPropertyItem
                {
                    CategoryPropertyId = p.CategoryPropertyId,
                    Value = p.Value
                }).ToList()
            }, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> RemoveProperty(
            Guid propertyId,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new RemoveProductVariantPropertyCommand { Id = propertyId }, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }
    }
}
