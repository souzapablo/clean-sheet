using CleanSheet.Application.Features.Career.Commands.Create;
using CleanSheet.Domain.Entities;

namespace CleanSheet.API.Endpoints;

public static class CareerEndpoints
{
    public static void MapCareerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/careers")
            .WithTags("Careers");

        group.MapPost("", CreateAsync);
    }

    private static IResult CreateAsync(CreateCareerInput input)
    {
        return TypedResults.Ok(new Career(Guid.NewGuid(), input.Manager));
    }
}