using CleanSheet.API.Extensions;
using CleanSheet.Application.Features.InitialTeams.Commands.AddInitialTeamPlayer;
using CleanSheet.Application.Features.InitialTeams.Commands.Create;
using CleanSheet.Application.Features.InitialTeams.Queries.Get;
using CleanSheet.Application.Features.InitialTeams.Queries.GetBySlug;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanSheet.API.Endpoints;

public static class InitialTeamEndpoints
{
    public static void MapInitialTeamEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/initial-teams")
            .WithTags("Initial teams");

        group.MapPost("", CreateAsync);
        group.MapGet("", GetAsync);
        group.MapGet("{slug}", GetBySlugAsync);
        group.MapPatch("{slug}/add-player", AddPlayerAsync);
    }

    private static async Task<IResult> CreateAsync(
        ISender sender,
        CreateInitialTeamRequest request)
    {
        var command = new CreateInitialTeamCommand(request.Name, request.Stadium);

        var result = await sender.Send(command);

        return TypedResults.Created($"/api/v1/initial-teams/{result.Data}", result);
    }

    private static async Task<IResult> GetAsync(
        ISender sender)
    {
        var query = new GetInitialTeamsQuery();

        var testResult = await sender.Send(query);

        return TypedResults.Ok(testResult);
    }

    private static async Task<IResult> GetBySlugAsync(
        ISender sender,
        string slug)
    {
        var query = new GetInitialTeamBySlugQuery(slug);

        var result = await sender.Send(query);

        if (!result.IsSuccess)
            return FailureExtensions.Handle(result);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> AddPlayerAsync(
        ISender sender,
        string slug,
        [FromBody] AddInitialTeamPlayerRequest request)
    {
        var command = new AddInitialTeamPlayerCommand(slug, request.Name, request.KitNumber, request.Overall,
            request.Birthday, request.Position);

        var result = await sender.Send(command);

        if (!result.IsSuccess)
            FailureExtensions.Handle(result);
        
        return TypedResults.Ok(result);
    }
}