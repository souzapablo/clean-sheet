using CleanSheet.Application.Features.Careers.Queries.GeyById;
using CleanSheet.Domain.Entities;
using FluentAssertions;

namespace CleanSheet.UnitTests.Features.Careers;

public class GetCareerByIdQueryHandlerTests
{
    private readonly ICareerRepository _careerRepository = Substitute.For<ICareerRepository>();

    [Fact(DisplayName = "Given a valid id should return career response")]
    public async Task Given_AValidId_Should_ReturnCareerResponse()
    {
        // Arrange
        var career = new Career(Guid.NewGuid(), "Ancelotti");
        var query = new GetCareerByIdQuery(Guid.NewGuid());

        _careerRepository.GetByIdAsync(Arg.Any<Guid>())
            .Returns(career);
        
        // Act
        var testResult = await QueryHandler.Handle(query, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeTrue();
        testResult.Data?.Manager.Should().NotBeNullOrEmpty();
    }
    private GetCareerByIdQueryHandler QueryHandler => new(_careerRepository);
}