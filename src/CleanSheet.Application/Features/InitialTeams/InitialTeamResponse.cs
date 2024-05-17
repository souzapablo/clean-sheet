using CleanSheet.Application.Features.Players;

namespace CleanSheet.Application.Features.InitialTeams;

public record InitialTeamResponse(
    long Id,
    string Name,
    string Stadium,
    IEnumerable<PlayerResponse> Players);