using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Queries.GetByUser;
public record GetCareerByUserIdQuery(
    long Id) : IRequest<Result<IEnumerable<CareerResponse>>>;