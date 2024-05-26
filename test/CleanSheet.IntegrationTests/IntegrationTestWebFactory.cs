using CleanSheet.API;
using CleanSheet.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Testcontainers.PostgreSql;
using Hash = BCrypt.Net.BCrypt;

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
        await CreateEntities(_connectionString);
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

    private static async Task CreateEntities(string connectionString)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();
        var passwordHash = Hash.HashPassword("Testing@123");
        var insertUserQuery = $"INSERT INTO users (name, email, password_hash, role, is_deleted) VALUES ('Test', 'test@email.com', '{passwordHash}', 0, false)";
        var insertCareerQuery = "INSERT INTO careers (manager, user_id, is_deleted, last_update) VALUES ('Ancelotti', 1, false, now())";
        var insertInitialTeamQuery = "INSERT INTO initial_teams (name, stadium, slug, is_deleted, players) VALUES ('Test Team', 'Test Arena', 'test', false, '[{\"Id\":0,\"Name\":\"Test Player\",\"Overall\":99,\"Birthday\":\"1979-05-22\",\"Position\":0,\"IsDeleted\":false,\"KitNumber\":99}]')";
        var insertTeamQuery = "INSERT INTO teams (name, stadium, career_id, is_deleted) VALUES ('Test Team', 'Test Arena', 1, false)";

        await using var cmd = new NpgsqlCommand(insertUserQuery, connection);
        await cmd.ExecuteNonQueryAsync();

        cmd.CommandText = insertCareerQuery;
        await cmd.ExecuteNonQueryAsync();

        cmd.CommandText = insertInitialTeamQuery;
        await cmd.ExecuteNonQueryAsync();

        cmd.CommandText = insertTeamQuery;
        await cmd.ExecuteNonQueryAsync();

        await connection.CloseAsync();
    }
}