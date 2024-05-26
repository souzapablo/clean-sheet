using CleanSheet.Application.Features.Players;

namespace CleanSheet.Application.Features.Teams;
public record TeamResponse(
    long Id,
    string Name,
    string Stadium,
    IEnumerable<PlayerDetailResponse> Players);