using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Commerce.AdminUsers.Application.AdminUsers.Commands.AddAdminUser;
using Modulith.Commerce.AdminUsers.Application.AdminUsers.Commands.DeleteAdminUser;
using Modulith.Commerce.AdminUsers.Application.AdminUsers.Commands.SyncAdminUser;
using Modulith.Commerce.AdminUsers.Application.AdminUsers.Commands.UpdateAdminUser;
using Modulith.Commerce.AdminUsers.Application.AdminUsers.Queries.GetAdminUser;
using Modulith.Commerce.AdminUsers.Application.AdminUsers.Queries.GetAdminUsers;
using Modulith.Commerce.AdminUsers.Presentation.AdminUsers.DTOs;
using Modulith.Commerce.AdminUsers.Presentation.Authorization;

namespace Modulith.Commerce.AdminUsers.Presentation.AdminUsers
{
    public static class AdminUserEndpoints
    {
        public static IEndpointRouteBuilder MapAdminUserEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("admin-users").WithTags("AdminUsers");

            group.MapGet("", GetAdminUsers).RequireAuthorization(AdminUserPolicies.AdminUserRead);
            group.MapGet("{id:guid}", GetAdminUser).RequireAuthorization(AdminUserPolicies.AdminUserRead);
            group.MapPost("", AddAdminUser).RequireAuthorization(AdminUserPolicies.AdminUserWrite);
            group.MapPut("{id:guid}", UpdateAdminUser).RequireAuthorization(AdminUserPolicies.AdminUserWrite);
            group.MapDelete("{id:guid}", DeleteAdminUser).RequireAuthorization(AdminUserPolicies.AdminUserDelete);
            group.MapPost("{id:guid}/sync", SyncAdminUser).RequireAuthorization(AdminUserPolicies.AdminUserWrite);

            return app;
        }

        private static async Task<IResult> GetAdminUsers(
            ISender sender,
            IMapper mapper,
            string? key = null,
            CancellationToken cancellationToken = default)
        {
            var query = new GetAdminUsersQuery(key);
            var result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(mapper.Map<List<AdminUserListItemResponseDto>>(result.Value));
        }

        private static async Task<IResult> GetAdminUser(
            Guid id,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            var query = new GetAdminUserQuery(id);
            var result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return Results.NotFound(result.Error);

            return Results.Ok(mapper.Map<AdminUserDetailResponseDto>(result.Value));
        }

        private static async Task<IResult> AddAdminUser(
            ISender sender,
            IMapper mapper,
            AddAdminUserRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var command = mapper.Map<AddAdminUserCommand>(request);
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok();
        }

        private static async Task<IResult> UpdateAdminUser(
            Guid id,
            ISender sender,
            IMapper mapper,
            UpdateAdminUserRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var command = mapper.Map<UpdateAdminUserCommand>(request) with { Id = id };
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> DeleteAdminUser(
            Guid id,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var command = new DeleteAdminUserCommand(id);
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> SyncAdminUser(
            Guid id,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var command = new SyncAdminUserCommand(id);
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }
    }
}
