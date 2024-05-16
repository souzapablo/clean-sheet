using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CleanSheet.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration => 
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}