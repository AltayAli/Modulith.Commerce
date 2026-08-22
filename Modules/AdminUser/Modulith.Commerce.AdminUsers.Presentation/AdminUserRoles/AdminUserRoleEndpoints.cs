using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Commerce.AdminUsers.Application.AdminUserRoles.Commands.AssignRoleToUser;
using Modulith.Commerce.AdminUsers.Application.AdminUserRoles.Commands.RevokeRoleFromUser;
using Modulith.Commerce.AdminUsers.Application.AdminUserRoles.Queries.GetUserRoles;
using Modulith.Commerce.AdminUsers.Presentation.AdminUserRoles.DTOs;
using Modulith.Commerce.AdminUsers.Presentation.Authorization;

namespace Modulith.Commerce.AdminUsers.Presentation.AdminUserRoles
{
    public static class AdminUserRoleEndpoints
    {
        public static IEndpointRouteBuilder MapAdminUserRoleEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("admin-users/{userId:guid}/roles").WithTags("AdminUserRoles");

            group.MapGet("", GetUserRoles).RequireAuthorization(AdminUserPolicies.AdminUserRoleRead);
            group.MapPost("", AssignRoleToUser).RequireAuthorization(AdminUserPolicies.AdminUserRoleWrite);
            group.MapDelete("{roleId:guid}", RevokeRoleFromUser).RequireAuthorization(AdminUserPolicies.AdminUserRoleDelete);

            return app;
        }

        private static async Task<IResult> GetUserRoles(
            Guid userId,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            var query = new GetUserRolesQuery(userId);
            var result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(mapper.Map<List<UserRoleListItemResponseDto>>(result.Value));
        }

        private static async Task<IResult> AssignRoleToUser(
            Guid userId,
            AssignRoleToUserRequestDto request,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            var command = mapper.Map<AssignRoleToUserCommand>(request) with { UserId = userId };
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Created();
        }

        private static async Task<IResult> RevokeRoleFromUser(
            Guid userId,
            Guid roleId,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var command = new RevokeRoleFromUserCommand(userId, roleId);
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }
    }
}
