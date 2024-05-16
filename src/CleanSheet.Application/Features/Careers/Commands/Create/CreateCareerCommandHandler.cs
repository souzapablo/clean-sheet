using CleanSheet.Domain.Entities;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Commands.Create;

public class CreateCareerCommandHandler : IRequestHandler<CreateCareerCommand, Guid>
{
    public async Task<Guid> Handle(CreateCareerCommand request, CancellationToken cancellationToken)
    {
        var newCareer = new Career(Guid.NewGuid(), request.Manager);

        return newCareer.Id;
    }
}