using CleanSheet.API.Extensions;
using CleanSheet.Application.Features.Careers.Commands.Create;
using CleanSheet.Application.Features.Careers.Commands.Delete;
using CleanSheet.Application.Features.Careers.Queries.Get;
using CleanSheet.Application.Features.Careers.Queries.GeyById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanSheet.API.Endpoints;

public static class CareerEndpoints
{
    public static void MapCareerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/careers")
            .RequireAuthorization()
            .WithTags("Careers");

        group.MapPost("", CreateAsync);
        group.MapGet("", GetAsync);
        group.MapGet("{id:long}", GetByIdAsync);
        group.MapDelete("{id:long}", DeleteAsync);
    }

    private static async Task<IResult> CreateAsync(
        CreateCareerRequest request, 
        ISender sender)
    {
        var command = new CreateCareerCommand(request.UserId, request.Manager, request.TeamSlug);

        var result = await sender.Send(command);
        
        return TypedResults.Created($"/api/v1/careers/{result.Data}", result);
    }

    private static async Task<IResult> GetAsync(ISender sender)
    {
        var query = new GetCareersQuery();

        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetByIdAsync(
        ISender sender, 
        [FromRoute] long id)
    {
        var query = new GetCareerByIdQuery(id);

        var result = await sender.Send(query);

        if (!result.IsSuccess)
            return FailureExtensions.Handle(result);
        
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> DeleteAsync(
        ISender sender,
        [FromRoute] long id)
    {
        var command = new DeleteCareerCommand(id);

        var result = await sender.Send(command);

        if (!result.IsSuccess)
            return FailureExtensions.Handle(result);
        
        return TypedResults.NoContent();
    }
}