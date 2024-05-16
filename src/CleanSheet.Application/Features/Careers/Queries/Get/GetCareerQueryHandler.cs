using CleanSheet.Domain.Repositories;
using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Queries.Get;

public class GetCareerQueryHandler(ICareerRepository careerRepository) 
    : IRequestHandler<GetCareersQuery, Result<IEnumerable<CareerResponse>>>
{
    public async Task<Result<IEnumerable<CareerResponse>>> Handle(GetCareersQuery request, CancellationToken cancellationToken)
    {
        var careers = await careerRepository.ListAsync(cancellationToken);

        var result = careers.Select(career =>
            new CareerResponse(
                career.Id,
                career.Manager,
                career.LastUpdate));

        return Result<IEnumerable<CareerResponse>>.Success(result);
    }
}