using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Repositories;
using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Commands.Create;

public class CreateCareerCommandHandler(ICareerRepository careerRepository) : IRequestHandler<CreateCareerCommand, TypedResult<Guid>>
{
    public async Task<TypedResult<Guid>> Handle(CreateCareerCommand request, CancellationToken cancellationToken)
    {
        var newCareer = new Career(Guid.NewGuid(), request.Manager);

        await careerRepository.AddAsync(newCareer, cancellationToken);
        
        return TypedResult<Guid>.Success(newCareer.Id);
    }
}