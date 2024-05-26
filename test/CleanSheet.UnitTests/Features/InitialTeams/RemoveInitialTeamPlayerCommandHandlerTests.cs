using CleanSheet.Application.Features.InitialTeams.Commands.RemoveInitialTeamPlayer;

namespace CleanSheet.UnitTests.Features.InitialTeams;

public class RemoveInitialTeamPlayerCommandHandlerTests
{
    private readonly IInitialTeamRepository _initialTeamRepository = Substitute.For<IInitialTeamRepository>();

    [Fact(DisplayName = "Remove initial player command handler should remove player from initial team given a valid player")]
    public async Task RemoveInitialPlayerCommandHandler_Should_RemovePlayerFromInitialTeam_Given_AValidPlayer()
    {
        // Arrange
        var initialTeam = new InitialTeam("Chelsea", "Stamford Bridge", "chelsea");
        initialTeam.AddPlayer(new Player("Cole Palmer", 20, 79, new DateOnly(2002, 05, 6), PlayerPosition.Rm));

        var command = new RemoveInitialTeamPlayerCommand("chelsea", "Cole Palmer");

        _initialTeamRepository.GetInitialTeamBySlugAsync(Arg.Any<string>())
            .Returns(initialTeam);

        // Act
        var testResult = await CommandHandler.Handle(command, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeTrue();
        testResult.Error.Should().BeNull();
        initialTeam.Players.Count.Should().Be(0);
    }

    [Fact(DisplayName = "Remove initial player command handler should return player not found in initial team given an invalid player")]
    public async Task RemoveInitialPlayerCommandHandler_Should_ReturnPlayerNotFoundInInitialTeam_Given_AnInvalidPlayer()
    {
        // Arrange
        var initialTeam = new InitialTeam("Chelsea", "Stamford Bridge", "chelsea");

        var command = new RemoveInitialTeamPlayerCommand("chelsea", "Cole Palmer");

        _initialTeamRepository.GetInitialTeamBySlugAsync(Arg.Any<string>())
            .Returns(initialTeam);

        // Act
        var testResult = await CommandHandler.Handle(command, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Error?.Code.Should().Be("InitialTeamPlayerNotFound");
    }

    [Fact(DisplayName = "Remove initial player command handler should return initial team not found team given an invalid slug")]
    public async Task RemoveInitialPlayerCommandHandler_Should_ReturnInitialTeamNotFound_Given_AnInvalidSlug()
    {
        // Arrange
        var command = new RemoveInitialTeamPlayerCommand("chelsea", "Cole Palmer");

        _initialTeamRepository.GetInitialTeamBySlugAsync(Arg.Any<string>())
            .ReturnsNull();

        // Act
        var testResult = await CommandHandler.Handle(command, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Error?.Code.Should().Be("InitialTeamNotFound");
    }
    private RemoveInitialTeamPlayerCommandHandler CommandHandler => new(_initialTeamRepository);
}
