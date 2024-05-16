using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Queries.GeyById;

public record GetCareerByIdQuery(Guid Id) : IRequest<TypedResult<CareerResponse>>;