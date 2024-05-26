using CleanSheet.Application.Features.Users.Commands.Create;
using CleanSheet.Domain.Enums;
using FluentAssertions;

namespace CleanSheet.IntegrationTests.Features.Users;
public class UsersTest(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    [Fact(DisplayName = "Create user command should create a new user given valid input")]
    public async Task CreateUserCommand_Should_CreateANewUser_Given_ValidInput()
    {
        // Arrange
        var query = new CreateUserCommand("James", "james@email.com", "Testing@123", UserRole.Admin);

        // Act
        var testResult = await Sender.Send(query);

        // Assert
        testResult.IsSuccess.Should().BeTrue();
        testResult.Data.Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "Create user command should return validation error given invalid input")]
    public async Task CreateUserCommand_Should_RetunValidationError_Given_InvalidInput()
    {
        // Arrange
        var query = new CreateUserCommand("Jes", "jamesemail.com", "Testing123", UserRole.Admin);

        // Act
        var testResult = await Sender.Send(query);

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Data.Should().Be(0);
        testResult.Error?.Code.Should().Be("ValidationError");
    }
}
