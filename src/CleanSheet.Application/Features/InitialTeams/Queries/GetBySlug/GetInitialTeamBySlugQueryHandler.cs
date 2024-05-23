using CleanSheet.Application.Features.Players;
using CleanSheet.Domain.Errors;
using CleanSheet.Domain.Repositories;
using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.InitialTeams.Queries.GetBySlug;
public class GetInitialTeamBySlugQueryHandler(
    IInitialTeamRepository initialTeamRepository) : IRequestHandler<GetInitialTeamBySlugQuery, Result<InitialTeamResponse>>
{
    public async Task<Result<InitialTeamResponse>> Handle(GetInitialTeamBySlugQuery request, CancellationToken cancellationToken)
    {
        var initialTeam = await initialTeamRepository.GetInitialTeamBySlugAsync(request.Slug, cancellationToken);

        if (initialTeam is null)
            return Result<InitialTeamResponse>.Failure(InitialTeamErrors.InitialTeamNotFound(request.Slug));

        var response = new InitialTeamResponse(
            initialTeam.Id,
            initialTeam.Name,
            initialTeam.Stadium,
            initialTeam.Players.Select(player => new PlayerResponse(player.Name)));

        return Result<InitialTeamResponse>.Success(response);
    }
}
