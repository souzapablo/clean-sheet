﻿using CleanSheet.Application.Features.Careers.Commands.Create;
using CleanSheet.Application.Features.Careers.Queries.Get;
using MediatR;

namespace CleanSheet.API.Endpoints;

public static class CareerEndpoints
{
    public static void MapCareerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/careers")
            .WithTags("Careers");

        group.MapPost("", CreateAsync);
        group.MapGet("", GetAsync);
    }

    private static async Task<IResult> CreateAsync(
        CreateCareerRequest request, 
        ISender sender)
    {
        var command = new CreateCareerCommand(request.Manager);

        var result = await sender.Send(command);
        
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetAsync(ISender sender)
    {
        var query = new GetCareersQuery();

        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }
}