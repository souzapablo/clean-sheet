namespace CleanSheet.Application.Features.Careers;
public record CareerResponse(
    long Id,
    string Manager,
    string CurrentTeam,
    DateTime LastUpdate);