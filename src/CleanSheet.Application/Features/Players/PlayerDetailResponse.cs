using CleanSheet.Domain.Enums;

namespace CleanSheet.Application.Features.Players;

public record PlayerDetailResponse(
    long Id,
    string Name,
    int Overall,
    int KitNumber,
    int Age,
    PlayerPosition PlayerPosition);