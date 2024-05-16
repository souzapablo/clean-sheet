namespace CleanSheet.Application.Features.Careers;

public record CareerResponse(
    Guid Id,
    string Manager,
    DateTime LastUpdate);