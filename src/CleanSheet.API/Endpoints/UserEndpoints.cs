using CleanSheet.API.Extensions;
using CleanSheet.Application.Features.Users.Commands.Create;
using MediatR;

namespace CleanSheet.API.Endpoints;

public static class UserEndpoints   
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/users");

        group.MapPost("", CreateAsync);
    }

    private static async Task<IResult> CreateAsync(
        ISender sender,
        CreateUserRequest request)
    {
        var command = new CreateUserCommand(
            request.Name,
            request.Email,
            request.Password,
            request.Role);

        var result = await sender.Send(command);

        if (!result.IsSuccess)
            return FailureExtensions.Handle(result);

        return TypedResults.Created($"/api/v1/user/{result.Data}", result);
    }
}
