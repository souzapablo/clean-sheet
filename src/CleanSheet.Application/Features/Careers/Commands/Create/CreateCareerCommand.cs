using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Commands.Create;

public record CreateCareerCommand(string Manager) : IRequest<Result<Guid>>;