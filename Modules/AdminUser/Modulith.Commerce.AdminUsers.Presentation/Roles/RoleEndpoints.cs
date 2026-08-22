using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Commerce.AdminUsers.Application.Roles.Commands.AddRole;
using Modulith.Commerce.AdminUsers.Application.Roles.Commands.DeleteRole;
using Modulith.Commerce.AdminUsers.Application.Roles.Commands.SyncRole;
using Modulith.Commerce.AdminUsers.Application.Roles.Commands.UpdateRole;
using Modulith.Commerce.AdminUsers.Application.Roles.Queries.GetRole;
using Modulith.Commerce.AdminUsers.Application.Roles.Queries.GetRoles;
using Modulith.Commerce.AdminUsers.Presentation.Authorization;
using Modulith.Commerce.AdminUsers.Presentation.Roles.DTOs;

namespace Modulith.Commerce.AdminUsers.Presentation.Roles
{
    public static class RoleEndpoints
    {
        public static IEndpointRouteBuilder MapRoleEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("roles").WithTags("Roles");

            group.MapGet("", GetRoles).RequireAuthorization(AdminUserPolicies.RoleRead);
            group.MapGet("{id:guid}", GetRole).RequireAuthorization(AdminUserPolicies.RoleRead);
            group.MapPost("", AddRole).RequireAuthorization(AdminUserPolicies.RoleWrite);
            group.MapPut("{id:guid}", UpdateRole).RequireAuthorization(AdminUserPolicies.RoleWrite);
            group.MapDelete("{id:guid}", DeleteRole).RequireAuthorization(AdminUserPolicies.RoleDelete);
            group.MapPost("{id:guid}/sync", SyncRole).RequireAuthorization(AdminUserPolicies.RoleWrite);

            return app;
        }

        private static async Task<IResult> GetRoles(
            ISender sender,
            IMapper mapper,
            string? key = null,
            CancellationToken cancellationToken = default)
        {
            var query = new GetRolesQuery(key);
            var result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(mapper.Map<List<RoleListItemResponseDto>>(result.Value));
        }

        private static async Task<IResult> GetRole(
            Guid id,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            var query = new GetRoleQuery(id);
            var result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return Results.NotFound(result.Error);

            return Results.Ok(mapper.Map<RoleDetailResponseDto>(result.Value));
        }

        private static async Task<IResult> AddRole(
            ISender sender,
            IMapper mapper,
            AddRoleRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var command = mapper.Map<AddRoleCommand>(request);
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok();
        }

        private static async Task<IResult> UpdateRole(
            Guid id,
            ISender sender,
            IMapper mapper,
            UpdateRoleRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var command = mapper.Map<UpdateRoleCommand>(request) with { Id = id };
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> DeleteRole(
            Guid id,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var command = new DeleteRoleCommand(id);
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> SyncRole(
            Guid id,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var command = new SyncRoleCommand(id);
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }
    }
}
