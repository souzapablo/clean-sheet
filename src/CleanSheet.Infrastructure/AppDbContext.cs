using System.Reflection;
using CleanSheet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanSheet.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : DbContext(options)
{
    public DbSet<Career> Careers { get; private set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}