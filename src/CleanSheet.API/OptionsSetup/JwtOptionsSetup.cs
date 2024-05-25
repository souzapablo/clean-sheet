using CleanSheet.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace CleanSheet.API.OptionsSetup;

public class JwtOptionsSetup(IConfiguration configuration)
    : IConfigureOptions<JwtOptions>
{
    private const string _sectionName = "Jwt";

    public void Configure(JwtOptions options)
    {
        configuration.GetSection(_sectionName).Bind(options);
    }
}
