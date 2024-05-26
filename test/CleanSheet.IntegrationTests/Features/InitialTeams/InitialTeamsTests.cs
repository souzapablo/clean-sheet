using CleanSheet.Application.Features.InitialTeams.Commands.AddInitialTeamPlayer;
using CleanSheet.Application.Features.InitialTeams.Commands.Create;
using CleanSheet.Application.Features.InitialTeams.Commands.RemoveInitialTeamPlayer;
using CleanSheet.Application.Features.InitialTeams.Queries.Get;
using CleanSheet.Application.Features.InitialTeams.Queries.GetBySlug;
using CleanSheet.Domain.Enums;
using FluentAssertions;

namespace CleanSheet.IntegrationTests.Features.InitialTeams;
public class InitialTeamsTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    [Fact(DisplayName = "Get initial teams query should list at least one initial team available in database")]
    public async Task GetInitialTeamsQuery_Should_ListAtLeastOneInitialTeamAvailableInDatabase()
    {
        // Arrange
        var query = new GetInitialTeamsQuery();
        
        // Act
        var testResult = await Sender.Send(query);
        
        // Assert
        testResult.Data.Should().NotBeEmpty();
        testResult.Data?.Count().Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "Create initial team command should create a new initial team in database given a valid input")]
    public async Task CreateInitialTeamCommand_Should_CreateANewInitialTeamInDatabase_Given_AValidInput()
    {
        // Arrange
        var command = new CreateInitialTeamCommand("Test Team", "Test Stadium");

        // Act
        var testResult = await Sender.Send(command);

        // Assert
        testResult.IsSuccess.Should().BeTrue();
        testResult.Data.Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "Create initial team command should return validation error given an invalid input")]
    public async Task CreateInitialTeamCommand_Should_ReturnValidationError_Given_AnInvalidInput()
    {
        // Arrange
        var command = new CreateInitialTeamCommand("Tem", "Tes");

        // Act
        var testResult = await Sender.Send(command);

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Data.Should().Be(0);
        testResult.Error?.Code.Should().Be("ValidationError");
    }

    [Fact(DisplayName = "Get initial team by slug query should return not found error given an invalid slug")]
    public async Task GeInitialTeamBySlugQuery_Should_ReturnNotFoundError_Given_AnInvalidSlug()
    {
        // Arrange
        var query = new GetInitialTeamBySlugQuery("invalid-slug");

        // Act
        var testResult = await Sender.Send(query);

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Error?.Code.Should().Be("InitialTeamNotFound");
    }

    [Fact(DisplayName = "Get initial team by slug query should return initial team given a valid slug")]
    public async Task GeInitialTeamBySlugQuery_Should_ReturnInitialTeam_Given_AValidSlug()
    {
        // Arrange
        var query = new GetInitialTeamBySlugQuery("test");

        // Act
        var testResult = await Sender.Send(query);

        // Assert
        testResult.IsSuccess.Should().BeTrue();
        testResult.Data.Should().NotBeNull();
    }

    [Fact(DisplayName = "Add player to initial team should return initial team not found error given invalid slug")]
    public async Task AddPlayerToInitialTeamCommand_Should_ReturnInitialTeamNotFoundError_Given_AnInvalidSlug()
    {

       // Arrange
        var command = new AddInitialTeamPlayerCommand("invalid-slug", "Test Player", 10, 80, new DateOnly(2000, 08, 08), PlayerPosition.Gk);

        // Act
        var testResult = await Sender.Send(command);

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Error?.Code.Should().Be("InitialTeamNotFound");
    }

    [Fact(DisplayName = "Add player to initial team should return validation error given invalid input")]
    public async Task AddPlayerToInitialTeamCommand_Should_ReturnValidationError_Given_AnInvalidInput()
    {

        // Arrange
        var command = new AddInitialTeamPlayerCommand("test", "Test Player", 100, 100, new DateOnly(2024, 08, 08), PlayerPosition.Gk);

        // Act
        var testResult = await Sender.Send(command);

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Error?.Code.Should().Be("ValidationError");
    }


    [Fact(DisplayName = "Add player to initial team should add player given a valid input")]
    public async Task AddPlayerToInitialTeamCommand_Should_AddPlayer_Given_AnValidInput()
    {

        // Arrange
        var command = new AddInitialTeamPlayerCommand("test", "New Player", 10, 80, new DateOnly(2000, 08, 08), PlayerPosition.Gk);

        // Act
        var testResult = await Sender.Send(command);

        // Assert
        testResult.IsSuccess.Should().BeTrue();
    }

    [Fact(DisplayName = "Remove player from initial team should return initial team not found error given invalid slug")]
    public async Task RemovePlayerFromInitialTeamCommand_Should_ReturnInitialTeamNotFoundError_Given_AnInvalidSlug()
    {

        // Arrange
        var command = new RemoveInitialTeamPlayerCommand("invalid-slug", "Test Player");

        // Act
        var testResult = await Sender.Send(command);

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Error?.Code.Should().Be("InitialTeamNotFound");
    }

    [Fact(DisplayName = "Remove player from initial team should return initial team player not found error given invalid input")]
    public async Task RemovePlayerToInitialTeamCommand_Should_ReturnInitialTeamPlayerNotFoundError_Given_AnInvalidInput()
    {

        // Arrange
        var command = new RemoveInitialTeamPlayerCommand("test", "Test Player");

        // Act
        var testResult = await Sender.Send(command);

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Error?.Code.Should().Be("InitialTeamPlayerNotFound");
    }


    [Fact(DisplayName = "Remove player from initial team should remove player given a valid input")]
    public async Task AddPlayerToInitialTeamCommand_Should_RemovePlayer_Given_AValidInput()
    {

        // Arrange
        var command = new RemoveInitialTeamPlayerCommand("test", "Test Player");

        // Act
        var testResult = await Sender.Send(command);

        // Assert
        testResult.IsSuccess.Should().BeTrue();
    }
}
