using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Commands.Create;

public record CreateCareerCommand(
    long UserId,
    string Manager, 
    string InitialTeam) : IRequest<Result<long>>;