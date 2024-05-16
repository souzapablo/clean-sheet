using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Repositories;
using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Commands.Create;

public class CreateCareerCommandHandler(ICareerRepository careerRepository) : IRequestHandler<CreateCareerCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCareerCommand request, CancellationToken cancellationToken)
    {
        var newCareer = new Career(Guid.NewGuid(), request.Manager);

        await careerRepository.AddAsync(newCareer, cancellationToken);
        
        return Result<Guid>.Success(newCareer.Id);
    }
}