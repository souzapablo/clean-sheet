using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Repositories;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Commands.Create;

public class CreateCareerCommandHandler(ICareerRepository careerRepository) : IRequestHandler<CreateCareerCommand, Guid>
{
    public async Task<Guid> Handle(CreateCareerCommand request, CancellationToken cancellationToken)
    {
        var newCareer = new Career(Guid.NewGuid(), request.Manager);

        await careerRepository.AddAsync(newCareer, cancellationToken);
        
        return newCareer.Id;
    }
}