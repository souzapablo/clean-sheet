using CleanSheet.Application.Features.Teams;

namespace CleanSheet.Application.Features.Careers;

public record CareerDetailResponse(
    long Id,
    string Manager,
    IEnumerable<TeamResponse> Teams,
    DateTime LastUpdate);