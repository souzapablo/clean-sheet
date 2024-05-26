using CleanSheet.Application.Features.Players;
using CleanSheet.Application.Features.Teams;
using CleanSheet.Domain.Errors;
using CleanSheet.Domain.Repositories;
using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Queries.GeyById;

public class GetCareerByIdQueryHandler(ICareerRepository careerRepository) 
    : IRequestHandler<GetCareerByIdQuery, Result<CareerDetailResponse>>
{
    public async Task<Result<CareerDetailResponse>> Handle(GetCareerByIdQuery request, CancellationToken cancellationToken)
    {
        var career = await careerRepository.GetByIdAsync(request.Id, cancellationToken);

        if (career is null)
            return Result<CareerDetailResponse>.Failure(CareerErrors.CareerNotFound(request.Id));

        var teamsResponse = career.Teams
            .Select(team => new TeamResponse(
                team.Id,
                team.Name,
                team.Stadium,
                team.Squad.Select(player => new PlayerDetailResponse(
                    player.Id, 
                    player.Name,
                    player.Overall,
                    player.KitNumber,
                    player.Age,
                    player.Position))
                .ToList()
            ));
        
        var response = new CareerDetailResponse(career.Id, career.Manager, teamsResponse, career.LastUpdate);
        
        return Result<CareerDetailResponse>.Success(response);
    }
}