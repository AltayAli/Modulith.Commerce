using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Commerce.Products.Application.CategoryProperties.Commands.UpdateCategoryProperty;
using Modulith.Commerce.Products.Application.CategoryProperties.Queries.GetCategoryProperties;
using Modulith.Commerce.Products.Presentation.Authorization;
using Modulith.Commerce.Products.Presentation.CategoryProperties.DTOs;

namespace Modulith.Commerce.Products.Presentation.CategoryProperties
{
    public static class CategoryPropertyEndpoints
    {
        public static IEndpointRouteBuilder MapCategoryPropertyEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("categories/{categoryId:guid}/properties").WithTags("CategoryProperties");

            group.MapGet("", GetCategoryProperties).RequireAuthorization(ProductPolicies.CategoryPropertyRead);
            group.MapPut("", SaveCategoryProperties).RequireAuthorization(ProductPolicies.CategoryPropertyWrite);

            return app;
        }

        private static async Task<IResult> GetCategoryProperties(
            Guid categoryId,
            ISender sender,
            IMapper mapper,
            string key = "",
            CancellationToken cancellationToken = default)
        {
            var query = new GetCategoryPropertiesQuery { CategoryId = categoryId, Key = key };
            var result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(mapper.Map<List<CategoryPropertyResponseDto>>(result.Value));
        }

        private static async Task<IResult> SaveCategoryProperties(
            Guid categoryId,
            ISender sender,
            IMapper mapper,
            SaveCategoryPropertiesRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new UpdateCategoryPropertyCommand
            {
                CategoryId = categoryId,
                Items = mapper.Map<List<UpdateCategoryPropertyCommandItem>>(request.Items)
            }, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }
    }
}
