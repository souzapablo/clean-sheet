using CleanSheet.Application.Features.InitialTeams.Commands.AddInitialTeamPlayer;

namespace CleanSheet.UnitTests.Features.InitialTeams;

public class AddInitialTeamPlayerCommandHandlerTests
{
    private readonly IInitialTeamRepository _initialTeamRepository = Substitute.For<IInitialTeamRepository>();

    [Fact(DisplayName = "Add initial team player command should add player to initial team given an existing slug")]
    public async Task AddInitialTeamPlayerCommand_Should_AddPlayerToInitialTeam_Given_AnExistingSlug()
    {
        // Arrange
        var command = new AddInitialTeamPlayerCommand("chelsea", "Palmer", 20, 79, new DateOnly(2002, 05, 6),
            PlayerPosition.Rm | PlayerPosition.Rw | PlayerPosition.Cam);
        
        var initialTeam = new InitialTeam("Chelsea", "Stamford Bridge", "chelsea");

        _initialTeamRepository.GetBySlugAsync(Arg.Any<string>())
            .Returns(initialTeam);

        // Act
        var testResult = await CommandHandler.Handle(command, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeTrue();
        testResult.Error.Should().BeNull();
    }
    
    [Fact(DisplayName = "Add initial team player command should return initial team not found given non existing slug")]
    public async Task Given_AnExistingSlug_Should_AddPlayerToInitialTeam()
    {
        // Arrange
        var command = new AddInitialTeamPlayerCommand("chelsea", "Palmer", 20, 79, new DateOnly(2002, 05, 6),
            PlayerPosition.Rm | PlayerPosition.Rw | PlayerPosition.Cam);

        _initialTeamRepository.GetBySlugAsync(Arg.Any<string>())
            .ReturnsNull();

        // Act
        var testResult = await CommandHandler.Handle(command, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Error?.Code.Should().Be("InitialTeamNotFound");
    }

    private AddInitialTeamPlayerCommandHandler CommandHandler => new(_initialTeamRepository);
}