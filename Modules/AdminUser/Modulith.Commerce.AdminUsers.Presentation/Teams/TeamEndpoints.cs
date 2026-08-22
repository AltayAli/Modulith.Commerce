using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Commerce.AdminUsers.Application.Teams.Commands.AddTeam;
using Modulith.Commerce.AdminUsers.Application.Teams.Commands.AddUserToTeam;
using Modulith.Commerce.AdminUsers.Application.Teams.Commands.DeleteTeam;
using Modulith.Commerce.AdminUsers.Application.Teams.Commands.RemoveUserFromTeam;
using Modulith.Commerce.AdminUsers.Application.Teams.Commands.TransferUserToTeam;
using Modulith.Commerce.AdminUsers.Application.Teams.Commands.UpdateTeam;
using Modulith.Commerce.AdminUsers.Presentation.Authorization;
using Modulith.Commerce.AdminUsers.Presentation.Teams.DTOs;

namespace Modulith.Commerce.AdminUsers.Presentation.Teams
{
    public static class TeamEndpoints
    {
        public static IEndpointRouteBuilder MapTeamEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("teams").WithTags("Teams");

            group.MapPost("", AddTeam).RequireAuthorization(AdminUserPolicies.TeamWrite);
            group.MapPut("{id:guid}", UpdateTeam).RequireAuthorization(AdminUserPolicies.TeamWrite);
            group.MapDelete("{id:guid}", DeleteTeam).RequireAuthorization(AdminUserPolicies.TeamDelete);
            group.MapPost("{id:guid}/members", AddUserToTeam).RequireAuthorization(AdminUserPolicies.TeamWrite);
            group.MapDelete("{id:guid}/members/{userId:guid}", RemoveUserFromTeam).RequireAuthorization(AdminUserPolicies.TeamWrite);
            group.MapPost("{id:guid}/transfer", TransferUserToTeam).RequireAuthorization(AdminUserPolicies.TeamWrite);

            return app;
        }

        private static async Task<IResult> AddTeam(
            AddTeamRequestDto request,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            var command = mapper.Map<AddTeamCommand>(request);
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Created();
        }

        private static async Task<IResult> UpdateTeam(
            Guid id,
            UpdateTeamRequestDto request,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            var command = mapper.Map<UpdateTeamCommand>(request) with { Id = id };
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> DeleteTeam(
            Guid id,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var command = new DeleteTeamCommand(id);
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> AddUserToTeam(
            Guid id,
            AddUserToTeamRequestDto request,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var command = new AddUserToTeamCommand(request.UserId, id);
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> RemoveUserFromTeam(
            Guid id,
            Guid userId,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var command = new RemoveUserFromTeamCommand(userId);
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> TransferUserToTeam(
            Guid id,
            TransferUserToTeamRequestDto request,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var command = new TransferUserToTeamCommand(request.UserId, id);
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }
    }
}
