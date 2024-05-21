using CleanSheet.Domain.Errors;
using CleanSheet.Domain.Repositories;
using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Queries.GeyById;

public class GetCareerByIdQueryHandler(ICareerRepository careerRepository) 
    : IRequestHandler<GetCareerByIdQuery, Result<CareerResponse>>
{
    public async Task<Result<CareerResponse>> Handle(GetCareerByIdQuery request, CancellationToken cancellationToken)
    {
        var career = await careerRepository.GetByIdAsync(request.Id, cancellationToken);

        if (career is null)
            return Result<CareerResponse>.Failure(CareerErrors.CareerNotFound(request.Id));
        
        var response = new CareerResponse(career.Id, career.Manager, career.LastUpdate);
        
        return Result<CareerResponse>.Success(response);
    }
}