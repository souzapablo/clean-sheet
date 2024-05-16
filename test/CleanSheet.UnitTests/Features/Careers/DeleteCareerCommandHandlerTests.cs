using CleanSheet.Application.Features.Careers.Commands.Delete;
using CleanSheet.Domain.Entities;
using FluentAssertions;

namespace CleanSheet.UnitTests.Features.Careers;

public class DeleteCareerCommandHandlerTests
{
    private readonly ICareerRepository _careerRepository = Substitute.For<ICareerRepository>();

    [Fact(DisplayName = "Given a valid id should delete career")]
    public async Task Given_AValidId_Should_DeleteCareer()
    {
        // Arrange
        var career = new Career(Guid.NewGuid(), "Ancelotti");
        var command = new DeleteCareerCommand(Guid.NewGuid());

        _careerRepository.GetByIdAsync(Arg.Any<Guid>())
            .Returns(career);
        
        // Act
        var testResult = await CommandHandler.Handle(command, new CancellationToken());
        
        // Assert
        testResult.IsSuccess.Should().BeTrue();
        career.IsDeleted.Should().BeTrue();
        await _careerRepository.Received(1).UpdateAsync(career);
    }
    
    [Fact(DisplayName = "Given an invalid id should return career not found error")]
    public async Task Given_AnInvalidId_Should_ReturnCareerNotFoundError()
    {
        // Arrange
        var command = new DeleteCareerCommand(Guid.NewGuid());

        _careerRepository.GetByIdAsync(Arg.Any<Guid>())
            .ReturnsNull();
        
        // Act
        var testResult = await CommandHandler.Handle(command, new CancellationToken());
        
        // Assert
        testResult.IsSuccess.Should().BeFalse();
        await _careerRepository.DidNotReceive().UpdateAsync(Arg.Any<Career>());
    }
    
    private DeleteCareerCommandHandler CommandHandler => new(_careerRepository);
}