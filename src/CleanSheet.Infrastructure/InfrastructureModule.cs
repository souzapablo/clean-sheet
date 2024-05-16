using CleanSheet.Domain.Repositories;
using CleanSheet.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanSheet.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(configuration =>
            configuration.UseInMemoryDatabase("clean-sheet-db"));

        services.AddRepositories();
        
        return services;
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICareerRepository, CareerRepository>();
    }
}