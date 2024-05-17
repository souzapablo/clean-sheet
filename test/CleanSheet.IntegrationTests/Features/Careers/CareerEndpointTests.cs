﻿using CleanSheet.Application.Features.Careers.Commands.Create;
using CleanSheet.Application.Features.Careers.Commands.Delete;
using CleanSheet.Application.Features.Careers.Queries.Get;
using CleanSheet.Application.Features.Careers.Queries.GeyById;
using FluentAssertions;

namespace CleanSheet.IntegrationTests.Features.Careers;

public class CareerEndpointTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    [Fact(DisplayName = "Get careers query should list at least one career available in database")]
    public async Task GetCareersQuery_Should_ListAtLeastOneCareerAvailableInDatabase()
    {
        // Arrange
        var command = new CreateCareerCommand("Ancelotti");
        await Sender.Send(command);
        var query = new GetCareersQuery();
        
        // Act
        var testResult = await Sender.Send(query);
        
        // Assert
        testResult.Data.Should().NotBeEmpty();
        testResult.Data?.Count().Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "Get career by id query should return career response given a valid id")]
    public async Task GetCareerByIdQuery_Should_ReturnCareerResponse_Given_AValidId()
    {
        // Arrange
        var command = new CreateCareerCommand("Tite");
        var createdCareer = await Sender.Send(command);
        var query = new GetCareerByIdQuery(createdCareer.Data);

        // Act
        var testResult = await Sender.Send(query);

        // Assert
        testResult.IsSuccess.Should().BeTrue();
        testResult.Data.Should().NotBeNull();
        testResult.Error.Should().BeNull();
    }
    
    [Fact(DisplayName = "Delete command should delete career given a valid id")]
    public async Task DeleteCareerCommand_Should_DeleteCareer_Given_AValidId()
    {
        // Arrange
        var command = new CreateCareerCommand("Jorge Jesus");
        var createdCareer = await Sender.Send(command);
        var query = new DeleteCareerCommand(createdCareer.Data);

        // Act
        var testResult = await Sender.Send(query);

        // Assert
        testResult.IsSuccess.Should().BeTrue();
    }
    
    [Fact(DisplayName = "Delete career command should return career not found error given an invalid id ")]
    public async Task DeleteCareerCommand_Should_ReturnCareerNotFoundError_Given_AnInvalidId()
    {
        // Arrange
        var query = new DeleteCareerCommand(Guid.NewGuid());

        // Act
        var testResult = await Sender.Send(query);

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Error?.Code.Should().Be("CareerNotFound");
    }
    
    [Fact(DisplayName = "Get career by query id should return CareerNotFound error given an invalid id")]
    public async Task GetCareerByIdQuery_Should_DeleteCareer_Given_AValidId()
    {
        // Arrange
        var query = new GetCareerByIdQuery(Guid.NewGuid());

        // Act
        var testResult = await Sender.Send(query);

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Data.Should().BeNull();
        testResult.Error?.Code.Should().Be("CareerNotFound");
    }

    [Fact(DisplayName = "Create career command should create a new career when input is valid")]
    public async Task CreateCareerCommand_Should_CreateANewCareer_When_InputIsValid()
    {
        // Arrange
        var command = new CreateCareerCommand("Abel Ferreira");
        
        // Act
        var testResult = await Sender.Send(command);
        
        // Assert
        testResult.IsSuccess.Should().BeTrue();
        testResult.Data.Should().NotBeEmpty();
        testResult.Error.Should().BeNull();
    }
    
    [Fact(DisplayName = "Create career command should return validation error when input is invalid")]
    public async Task CreateCareerCommand_Should_ReturnValidationError_When_InputIsInvalid()
    {
        // Arrange
        var command = new CreateCareerCommand("GG");
        
        // Act
        var testResult = await Sender.Send(command);
        
        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Data.Should().BeEmpty();
        testResult.Error.Should().NotBeNull();
    }
}