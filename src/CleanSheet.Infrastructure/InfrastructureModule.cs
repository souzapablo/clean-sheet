using CleanSheet.Domain.Repositories;
using CleanSheet.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanSheet.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<AppDbContext>(config =>
            config.UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention());
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        

        services.AddRepositories();
        
        return services;
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICareerRepository, CareerRepository>();
        services.AddScoped<IInitialTeamRepository, InitialTeamRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}