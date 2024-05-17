using CleanSheet.Application.Features.InitialTeams.Commands.Create;
using MediatR;

namespace CleanSheet.API.Endpoints;

public static class InitialTeamEndpoints
{
    public static void MapInitialTeamEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/initial-teams")
            .WithTags("Initial teams");

        group.MapPost("", CreateAsync);
    }

    private static async Task<IResult> CreateAsync(
        ISender sender,
        CreateInitialTeamRequest request)
    {
        var command = new CreateInitialTeamCommand(request.Name, request.Stadium);

        var result = await sender.Send(command);

        return TypedResults.Created($"/api/v1/initial-teams/{result.Data}", result);
    }
}