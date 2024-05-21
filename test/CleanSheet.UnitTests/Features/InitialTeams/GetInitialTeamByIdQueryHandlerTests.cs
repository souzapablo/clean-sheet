using CleanSheet.Application.Features.InitialTeams.Queries.GetById;
using CleanSheet.Domain.Entities;
using FluentAssertions;

namespace CleanSheet.UnitTests.Features.InitialTeams;
public class GetInitialTeamByIdQueryHandlerTests
{
    private readonly IInitialTeamRepository _initialTeamRepository = Substitute.For<IInitialTeamRepository>();

    [Fact(DisplayName = "Given a valid slug should return initial team response")]
    public async Task Given_AValidSlug_Should_ReturnInitialTeamResponse()
    {
        // Arrange
        var initialTeam = new InitialTeam("Chelsea", "Stamford Bridge", "chelsea");

        var query = new GetInitialTeamBySlugQuery("chelsea");

        _initialTeamRepository.GetInitialTeamBySlugAsync(Arg.Any<string>())
            .Returns(initialTeam);

        // Act
        var testResult = await QueryHandler.Handle(query, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeTrue();
        testResult.Data?.Name.Should().NotBeNullOrEmpty();
    }

    [Fact(DisplayName = "Given an invalid slug should return InitialTeamNotFoundError")]
    public async Task Given_AnInvalidSlug_Should_ReturnInitialTeamNotFoundError()
    {
        // Arrange
        var initialTeam = new InitialTeam("Chelsea", "Stamford Bridge", "chelsea");

        var query = new GetInitialTeamBySlugQuery("chelsea");

        _initialTeamRepository.GetInitialTeamBySlugAsync(Arg.Any<string>())
            .ReturnsNull();

        // Act
        var testResult = await QueryHandler.Handle(query, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Data.Should().BeNull();
        testResult.Error?.Code.Should().Be("InitialTeamNotFound");
    }

    private GetInitialTeamBySlugQueryHandler QueryHandler => new(_initialTeamRepository);
}
