using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Commerce.Products.Application.Brands.Commands.AddBrand;
using Modulith.Commerce.Products.Application.Brands.Commands.RemoveBrand;
using Modulith.Commerce.Products.Application.Brands.Commands.UpdateBrand;
using Modulith.Commerce.Products.Application.Brands.Queries.GetBrand;
using Modulith.Commerce.Products.Application.Brands.Queries.GetBrands;
using Modulith.Commerce.Products.Presentation.Authorization;
using Modulith.Commerce.Products.Presentation.Brands.DTOs;

namespace Modulith.Commerce.Products.Presentation.Brands
{
    public static class BrandEndpoints
    {
        public static IEndpointRouteBuilder MapBrandEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("brands").WithTags("Brands");

            group.MapGet("", GetBrands).RequireAuthorization(ProductPolicies.BrandRead);
            group.MapGet("{id:guid}", GetBrand).RequireAuthorization(ProductPolicies.BrandRead);
            group.MapPost("", AddBrand).RequireAuthorization(ProductPolicies.BrandWrite);
            group.MapPut("{id:guid}", UpdateBrand).RequireAuthorization(ProductPolicies.BrandWrite);
            group.MapDelete("{id:guid}", RemoveBrand).RequireAuthorization(ProductPolicies.BrandDelete);

            return app;
        }

        private static async Task<IResult> GetBrands(
            ISender sender,
            IMapper mapper,
            string key = "",
            CancellationToken cancellationToken = default)
        {
            var query = new GetBrandsQuery { Key = key };
            var result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(mapper.Map<List<BrandResponseDto>>(result.Value));
        }

        private static async Task<IResult> GetBrand(
            Guid id,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            var query = new GetBrandQuery { Id = id };
            var result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return Results.NotFound(result.Error);

            return Results.Ok(mapper.Map<BrandResponseDto>(result.Value));
        }

        private static async Task<IResult> AddBrand(
            ISender sender,
            IMapper mapper,
            BrandRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(mapper.Map<AddBrandCommand>(request), cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok();
        }

        private static async Task<IResult> UpdateBrand(
            Guid id,
            ISender sender,
            IMapper mapper,
            BrandRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var command = mapper.Map<UpdateBrandCommand>(request) with { Id = id };
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> RemoveBrand(
            Guid id,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new RemoveBrandCommand { Id = id }, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }
    }
}
