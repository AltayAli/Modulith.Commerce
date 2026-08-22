using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Commerce.AdminUsers.Application.Departments.Commands.AddDepartment;
using Modulith.Commerce.AdminUsers.Application.Departments.Commands.DeleteDepartment;
using Modulith.Commerce.AdminUsers.Application.Departments.Commands.UpdateDepartment;
using Modulith.Commerce.AdminUsers.Application.Departments.Queries.GetDepartment;
using Modulith.Commerce.AdminUsers.Application.Departments.Queries.GetDepartments;
using Modulith.Commerce.AdminUsers.Presentation.Authorization;
using Modulith.Commerce.AdminUsers.Presentation.Departments.DTOs;

namespace Modulith.Commerce.AdminUsers.Presentation.Departments
{
    public static class DepartmentEndpoints
    {
        public static IEndpointRouteBuilder MapDepartmentEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("departments").WithTags("Departments");

            group.MapGet("", GetDepartments).RequireAuthorization(AdminUserPolicies.DepartmentRead);
            group.MapGet("{id:guid}", GetDepartment).RequireAuthorization(AdminUserPolicies.DepartmentRead);
            group.MapPost("", AddDepartment).RequireAuthorization(AdminUserPolicies.DepartmentWrite);
            group.MapPut("{id:guid}", UpdateDepartment).RequireAuthorization(AdminUserPolicies.DepartmentWrite);
            group.MapDelete("{id:guid}", DeleteDepartment).RequireAuthorization(AdminUserPolicies.DepartmentDelete);

            return app;
        }

        private static async Task<IResult> GetDepartments(
            ISender sender,
            IMapper mapper,
            string? key = null,
            CancellationToken cancellationToken = default)
        {
            var query = new GetDepartmentsQuery(key);
            var result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(mapper.Map<List<DepartmentListItemResponseDto>>(result.Value));
        }

        private static async Task<IResult> GetDepartment(
            Guid id,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            var query = new GetDepartmentQuery(id);
            var result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return Results.NotFound(result.Error);

            return Results.Ok(mapper.Map<DepartmentDetailResponseDto>(result.Value));
        }

        private static async Task<IResult> AddDepartment(
            AddDepartmentRequestDto request,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            var command = mapper.Map<AddDepartmentCommand>(request);
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Created();
        }

        private static async Task<IResult> UpdateDepartment(
            Guid id,
            UpdateDepartmentRequestDto request,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            var command = mapper.Map<UpdateDepartmentCommand>(request) with { Id = id };
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> DeleteDepartment(
            Guid id,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var command = new DeleteDepartmentCommand(id);
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }
    }
}
