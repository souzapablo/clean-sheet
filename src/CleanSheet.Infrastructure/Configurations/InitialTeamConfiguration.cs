using CleanSheet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanSheet.Infrastructure.Configurations;

public class InitialTeamConfiguration : IEntityTypeConfiguration<InitialTeam>
{
    public void Configure(EntityTypeBuilder<InitialTeam> builder)
    {
        builder.HasQueryFilter(initialTeam => !initialTeam.IsDeleted);

        builder.HasIndex(initialTeam => initialTeam.Slug)
            .IsUnique();
        
        builder.Property(initialTeam => initialTeam.Players)
            .HasColumnType("jsonb");
    }
}