using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Application.Products.Commands.ArchiveProduct;
using Modulith.Commerce.Products.Application.Products.Commands.CreateProduct;
using Modulith.Commerce.Products.Application.Products.Commands.PublishProduct;
using Modulith.Commerce.Products.Application.Products.Commands.RemoveProduct;
using Modulith.Commerce.Products.Application.Products.Commands.UnpublishProduct;
using Modulith.Commerce.Products.Application.Products.Commands.UpdateProduct;
using Modulith.Commerce.Products.Application.Products.Queries.GetProduct;
using Modulith.Commerce.Products.Application.Products.Queries.GetProducts;
using Modulith.Commerce.Products.Presentation.Authorization;
using Modulith.Commerce.Products.Presentation.Products.DTOs;

namespace Modulith.Commerce.Products.Presentation.Products
{
    public static class ProductEndpoints
    {
        public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("products").WithTags("Products");

            group.MapGet("", GetProducts).RequireAuthorization(ProductPolicies.ProductRead);
            group.MapGet("{id:guid}", GetProduct).RequireAuthorization(ProductPolicies.ProductRead);
            group.MapPost("", AddProduct).RequireAuthorization(ProductPolicies.ProductWrite);
            group.MapPut("{id:guid}", UpdateProduct).RequireAuthorization(ProductPolicies.ProductWrite);
            group.MapDelete("{id:guid}", RemoveProduct).RequireAuthorization(ProductPolicies.ProductDelete);
            group.MapPatch("{id:guid}/status", UpdateProductStatus).RequireAuthorization(ProductPolicies.ProductWrite);

            return app;
        }

        private static async Task<IResult> GetProducts(
            ISender sender,
            IMapper mapper,
            string key = "",
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new GetProductsQuery { Key = key }, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(mapper.Map<List<ProductListResponseDto>>(result.Value));
        }

        private static async Task<IResult> GetProduct(
            Guid id,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new GetProductQuery { Id = id }, cancellationToken);

            if (result.IsFailure)
                return Results.NotFound(result.Error);

            return Results.Ok(mapper.Map<ProductDetailResponseDto>(result.Value));
        }

        private static async Task<IResult> AddProduct(
            ISender sender,
            IMapper mapper,
            AddProductRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(mapper.Map<CreateProductCommand>(request), cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Created($"products/{result.Value}", result.Value);
        }

        private static async Task<IResult> UpdateProduct(
            Guid id,
            ISender sender,
            IMapper mapper,
            UpdateProductRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(
                mapper.Map<UpdateProductCommand>(request) with { Id = id },
                cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> RemoveProduct(
            Guid id,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new RemoveProductCommand { Id = id }, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> UpdateProductStatus(
            Guid id,
            ISender sender,
            UpdateProductStatusRequestDto request,
            CancellationToken cancellationToken = default)
        {
            Result result = request.Status?.Trim().ToLowerInvariant() switch
            {
                "active" => await sender.Send(new PublishProductCommand { Id = id }, cancellationToken),
                "inactive" => await sender.Send(new UnpublishProductCommand { Id = id }, cancellationToken),
                "archived" => await sender.Send(new ArchiveProductCommand { Id = id }, cancellationToken),
                _ => Result.Failure(new Error(
                    "Product.InvalidStatus",
                    "Invalid status value. Allowed values are Active, Inactive, Archived."))
            };

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }
    }
}
