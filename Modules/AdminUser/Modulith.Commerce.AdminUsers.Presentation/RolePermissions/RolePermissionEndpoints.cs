using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Commerce.AdminUsers.Application.RolePermissions.Commands.AddRolePermission;
using Modulith.Commerce.AdminUsers.Application.RolePermissions.Commands.DeleteRolePermission;
using Modulith.Commerce.AdminUsers.Application.RolePermissions.Queries.GetRolePermissions;
using Modulith.Commerce.AdminUsers.Presentation.Authorization;
using Modulith.Commerce.AdminUsers.Presentation.RolePermissions.DTOs;

namespace Modulith.Commerce.AdminUsers.Presentation.RolePermissions
{
    public static class RolePermissionEndpoints
    {
        public static IEndpointRouteBuilder MapRolePermissionEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("roles/{roleId:guid}/permissions").WithTags("RolePermissions");

            group.MapGet("", GetRolePermissions).RequireAuthorization(AdminUserPolicies.RolePermissionRead);
            group.MapPost("", AddRolePermission).RequireAuthorization(AdminUserPolicies.RolePermissionWrite);
            group.MapDelete("{permissionId:guid}", DeleteRolePermission).RequireAuthorization(AdminUserPolicies.RolePermissionDelete);

            return app;
        }

        private static async Task<IResult> GetRolePermissions(
            Guid roleId,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            var query = new GetRolePermissionsQuery(roleId);
            var result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(mapper.Map<List<RolePermissionListItemResponseDto>>(result.Value));
        }

        private static async Task<IResult> AddRolePermission(
            Guid roleId,
            AddRolePermissionRequestDto request,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            var command = mapper.Map<AddRolePermissionCommand>(request) with { RoleId = roleId };
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Created();
        }

        private static async Task<IResult> DeleteRolePermission(
            Guid roleId,
            Guid permissionId,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var command = new DeleteRolePermissionCommand(permissionId);
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }
    }
}
