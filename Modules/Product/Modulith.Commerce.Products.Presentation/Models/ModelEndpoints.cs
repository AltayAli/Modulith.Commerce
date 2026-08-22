using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Commerce.Products.Application.Models.Commands.AddModel;
using Modulith.Commerce.Products.Application.Models.Commands.RemoveModel;
using Modulith.Commerce.Products.Application.Models.Commands.UpdateModel;
using Modulith.Commerce.Products.Application.Models.Queries.GetModels;
using Modulith.Commerce.Products.Presentation.Authorization;
using Modulith.Commerce.Products.Presentation.Models.DTOs;

namespace Modulith.Commerce.Products.Presentation.Models
{
    public static class ModelEndpoints
    {
        public static IEndpointRouteBuilder MapModelEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("brands/{brandId:guid}/models").WithTags("Models");

            group.MapGet("", GetModels).RequireAuthorization(ProductPolicies.ModelRead);
            group.MapPost("", AddModel).RequireAuthorization(ProductPolicies.ModelWrite);
            group.MapPut("{id:guid}", UpdateModel).RequireAuthorization(ProductPolicies.ModelWrite);
            group.MapDelete("{id:guid}", RemoveModel).RequireAuthorization(ProductPolicies.ModelDelete);

            return app;
        }

        private static async Task<IResult> GetModels(
            Guid brandId,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            var query = new GetModelsQuery { BrandId = brandId };
            var result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(mapper.Map<List<ModelResponseDto>>(result.Value));
        }

        private static async Task<IResult> AddModel(
            Guid brandId,
            ISender sender,
            IMapper mapper,
            ModelRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var command = mapper.Map<AddModelCommand>(request) with { BrandId = brandId };
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok();
        }

        private static async Task<IResult> UpdateModel(
            Guid brandId,
            Guid id,
            ISender sender,
            IMapper mapper,
            ModelRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var command = mapper.Map<UpdateModelCommand>(request) with { Id = id, BrandId = brandId };
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> RemoveModel(
            Guid id,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new RemoveModelCommand { Id = id }, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }
    }
}
