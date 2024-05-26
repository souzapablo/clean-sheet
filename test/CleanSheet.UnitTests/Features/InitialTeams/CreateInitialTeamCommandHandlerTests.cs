using CleanSheet.Application.Features.InitialTeams.Commands.Create;

namespace CleanSheet.UnitTests.Features.InitialTeams;

public class CreateInitialTeamCommandHandlerTests
{
    private readonly IInitialTeamRepository _initialTeamRepository = Substitute.For<IInitialTeamRepository>();

    [Fact(DisplayName = "Create initial team command handler should create a new initial team when input is valid")]
    public async Task CreateInitialTeamCommandHandler_Should_CreateANewInitialTeam_When_InputIsValid()
    {
        // Arrange
        var command = new CreateInitialTeamCommand("Chelsea", "Stamford Bridge");

        _initialTeamRepository.GetInitialTeamBySlugAsync(Arg.Any<string>())
            .ReturnsNull();
        
        // Act
        var testResult = await CommandHandler.Handle(command, new CancellationToken());
        
        // Assert
        testResult.IsSuccess.Should().BeTrue();
        testResult.Error?.Should().BeNull();
    }
   
    [Fact(DisplayName = "Create initial team command handler should return slug already exists error when initial team already exists")]
    public async Task CreateInitialTeamCommandHandler_Should_ReturnSlugAlreadyExistsError_When_InitialTeamAlreadyExists()
    {
        // Arrange
        var command = new CreateInitialTeamCommand("Chelsea", "Stamford Bridge");
        var initialTeam = new InitialTeam("Chelsea", "Stamford Bridge", "chelsea");

        _initialTeamRepository.GetInitialTeamBySlugAsync(Arg.Any<string>())
            .Returns(initialTeam);
        
        // Act
        var testResult = await CommandHandler.Handle(command, new CancellationToken());
        
        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Error?.Code.Should().Be("InitialTeamAlreadyExists");
    } 
    private CreateInitialTeamCommandHandler CommandHandler => new(_initialTeamRepository);
}