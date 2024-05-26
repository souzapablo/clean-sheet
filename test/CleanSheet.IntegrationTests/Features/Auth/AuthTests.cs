using CleanSheet.Application.Features.Auth;
using FluentAssertions;

namespace CleanSheet.IntegrationTests.Features.Auth;
public class Auth(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    [Fact(DisplayName = "Login command should return token given valid credentials")]
    public async Task LoginCommand_Should_ReturnToken_Given_ValidCredentials()
    {
        // Arrange
        var query = new LoginCommand("test@email.com", "Testing@123");

        // Act
        var testResult = await Sender.Send(query);

        // Assert
        testResult.IsSuccess.Should().BeTrue();
        testResult.Data.Should().NotBeNull();
    }

    [Fact(DisplayName = "Login command should return invalid credentials error given invalid e-mail")]
    public async Task LoginCommand_Should_ReturnInvalidCredentialsError_Given_InvalidEmail()
    {
        // Arrange
        var query = new LoginCommand("testing@email.com", "Testing@123");

        // Act
        var testResult = await Sender.Send(query);

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Data.Should().BeNull();
        testResult.Error?.Code.Should().Be("InvalidCredentials");
    }

    [Fact(DisplayName = "Login command should return invalid credentials error given invalid password")]
    public async Task LoginCommand_Should_ReturnInvalidCredentialsError_Given_InvalidPassword()
    {
        // Arrange
        var query = new LoginCommand("test@email.com", "Testing123");

        // Act
        var testResult = await Sender.Send(query);

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Data.Should().BeNull();
        testResult.Error?.Code.Should().Be("InvalidCredentials");
    }
}
