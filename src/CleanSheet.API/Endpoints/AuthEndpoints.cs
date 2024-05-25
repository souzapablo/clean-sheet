using CleanSheet.API.Extensions;
using CleanSheet.Application.Features.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanSheet.API.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/auth")
            .WithTags("Auth");

        group.MapPost("/login", LoginAsync);
    }

    private static async Task<IResult> LoginAsync(
        ISender sender,
        [FromBody] LoginRequest request)
    {
        var command = new LoginCommand(request.Email, request.Password);

        var result = await sender.Send(command);

        if (!result.IsSuccess)
            return FailureExtensions.Handle(result);

        return TypedResults.Ok(result);
    }
}
