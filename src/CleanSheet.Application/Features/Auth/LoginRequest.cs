namespace CleanSheet.Application.Features.Auth;
public record LoginRequest(
    string Email,
    string Password);