using CleanSheet.Application.Features.Careers.Commands.Create;
using FluentAssertions;


namespace CleanSheet.UnitTests.Features.Careers;

public class CreateCareerCommandTests
{
    private readonly ICareerRepository _careerRepository = Substitute.For<ICareerRepository>();

    [Fact(DisplayName = "Given a valid input should create a new career")]
    public async Task Given_AValidInput_Should_CreateANewCareer()
    {
        // Arrange
        var command = new CreateCareerCommand("Carlo Ancelotti");
        
        // Act
        var testResult = await CommandHandler.Handle(command, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeTrue();
        testResult.Data.Should().BeGreaterThan(0);
    }

    private CreateCareerCommandHandler CommandHandler => new(_careerRepository);
}