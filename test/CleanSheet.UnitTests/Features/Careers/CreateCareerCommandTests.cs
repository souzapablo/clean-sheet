using CleanSheet.Application.Features.Careers.Commands.Create;


namespace CleanSheet.UnitTests.Features.Careers;

public class CreateCareerCommandTests
{
    private readonly ICareerRepository _careerRepository = Substitute.For<ICareerRepository>();
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();

    [Fact(DisplayName = "Given a valid input should create a new career")]
    public async Task Given_AValidInput_Should_CreateANewCareer()
    {
        // Arrange
        var user = new User("Teste", "teste", "teste", UserRole.Admin);
        var command = new CreateCareerCommand(1L, "Carlo Ancelotti");
        
        _userRepository.GetByIdAsync(Arg.Any<long>())
            .Returns(user);

        // Act
        var testResult = await CommandHandler.Handle(command, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeTrue();
    }

    [Fact(DisplayName = "Given an invalid user should return user not found error")]
    public async Task Given_AnInalidUser_Should_ReturnUserNotFoundError()
    {
        // Arrange
        var command = new CreateCareerCommand(1L, "Carlo Ancelotti");

        _userRepository.GetByIdAsync(Arg.Any<long>())
            .ReturnsNull();

        // Act
        var testResult = await CommandHandler.Handle(command, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Error?.Code.Should().Be("UserNotFound");
    }

    private CreateCareerCommandHandler CommandHandler => new(_careerRepository, _userRepository);
}