using CleanSheet.Application.Features.Careers.Commands.Create;


namespace CleanSheet.UnitTests.Features.Careers;

public class CreateCareerCommandTests
{
    private readonly ICareerRepository _careerRepository = Substitute.For<ICareerRepository>();
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly IInitialTeamRepository _initialTeamRepository = Substitute.For<IInitialTeamRepository>();

    [Fact(DisplayName = "Given a valid input should create a new career")]
    public async Task Given_AValidInput_Should_CreateANewCareer()
    {
        // Arrange
        var user = new User("Teste", "teste", "teste", UserRole.Admin);
        var initialTeam = new InitialTeam("Test", "Test", "test");
        initialTeam.AddPlayer(new Player("Teste", 1, 10, new DateOnly(2000, 12, 12), PlayerPosition.Gk));

        var command = new CreateCareerCommand(1L, "Carlo Ancelotti", "test");
        
        _userRepository.GetByIdAsync(Arg.Any<long>())
            .Returns(user);

        _initialTeamRepository.GetBySlugAsync(Arg.Any<string>())
            .Returns(initialTeam);

        // Act
        var testResult = await CommandHandler.Handle(command, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeTrue();
    }

    [Fact(DisplayName = "Given an invalid user should return user not found error")]
    public async Task Given_AnInvalidUser_Should_ReturnUserNotFoundError()
    {
        // Arrange
        var command = new CreateCareerCommand(1L, "Carlo Ancelotti", "test");

        _userRepository.GetByIdAsync(Arg.Any<long>())
            .ReturnsNull();

        // Act
        var testResult = await CommandHandler.Handle(command, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Error?.Code.Should().Be("UserNotFound");
    }

    [Fact(DisplayName = "Given an invalid initial team should return initial team not found error")]
    public async Task Given_AnInvalidInitialTeam_Should_ReturnInitialTeamNotFoundError()
    {
        // Arrange
        var user = new User("Teste", "teste", "teste", UserRole.Admin);

        var command = new CreateCareerCommand(1L, "Carlo Ancelotti", "test");

        _userRepository.GetByIdAsync(Arg.Any<long>())
            .Returns(user);

        // Act
        var testResult = await CommandHandler.Handle(command, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Error?.Code.Should().Be("InitialTeamNotFound");
    }

    private CreateCareerCommandHandler CommandHandler => new(_careerRepository, _userRepository, _initialTeamRepository);
}