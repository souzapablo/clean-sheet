namespace CleanSheet.Infrastructure.Options;
public class JwtOptions
{
    public string Secret { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public string Issuer { get; init; } = null!;
}