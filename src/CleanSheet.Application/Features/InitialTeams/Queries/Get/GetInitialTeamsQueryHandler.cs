using CleanSheet.Application.Features.Players;
using CleanSheet.Domain.Repositories;
using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.InitialTeams.Queries.Get;

public class GetInitialTeamsQueryHandler(IInitialTeamRepository initialTeamRepository)
    : IRequestHandler<GetInitialTeamsQuery, Result<IEnumerable<InitialTeamResponse>>>
{
    public async Task<Result<IEnumerable<InitialTeamResponse>>> Handle(GetInitialTeamsQuery request,
        CancellationToken cancellationToken)
    {
        var initialTeams = await initialTeamRepository.ListAsync(cancellationToken);

        var response = initialTeams.Select(initialTeam =>
            new InitialTeamResponse(
                initialTeam.Id,
                initialTeam.Name,
                initialTeam.Stadium,
                initialTeam.Players
                    .Select(player => 
                        new PlayerResponse(player.Name))));
        
        return Result<IEnumerable<InitialTeamResponse>>.Success(response);
    }
}