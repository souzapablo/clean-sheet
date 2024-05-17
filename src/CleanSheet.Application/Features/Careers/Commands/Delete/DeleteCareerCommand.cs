using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Commands.Delete;

public record DeleteCareerCommand(long Id) : IRequest<Result>;