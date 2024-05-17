using CleanSheet.API;
using CleanSheet.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Testcontainers.PostgreSql;


namespace CleanSheet.IntegrationTests;

public class IntegrationTestWebAppFactory 
    : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:16.3-alpine3.19")
        .WithDatabase("clean_sheet")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();
    private string _connectionString = string.Empty;
    
    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        _connectionString = _dbContainer.GetConnectionString();
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await dbContext.Database.MigrateAsync();
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
    }


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services
                .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<AppDbContext>));

            if (descriptor is not null)
                services.Remove(descriptor);

            services.AddDbContext<AppDbContext>(options =>
            {
                options
                    .UseNpgsql(_connectionString)
                    .UseSnakeCaseNamingConvention();
            });
        });
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }
}