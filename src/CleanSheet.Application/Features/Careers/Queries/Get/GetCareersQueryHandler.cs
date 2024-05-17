using CleanSheet.Domain.Repositories;
using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Queries.Get;

public class GetCareersQueryHandler(ICareerRepository careerRepository) 
    : IRequestHandler<GetCareersQuery, TypedResult<IEnumerable<CareerResponse>>>
{
    public async Task<TypedResult<IEnumerable<CareerResponse>>> Handle(GetCareersQuery request, CancellationToken cancellationToken)
    {
        var careers = await careerRepository.ListAsync(cancellationToken);

        var result = careers.Select(career =>
            new CareerResponse(
                career.Id,
                career.Manager,
                career.LastUpdate));

        return TypedResult<IEnumerable<CareerResponse>>.Success(result);
    }
}