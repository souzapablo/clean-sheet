using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Queries.Get;

public record GetCareersQuery() : IRequest<Result<IEnumerable<CareerResponse>>>;