﻿// <auto-generated />
using System;
using System.Collections.Generic;
using CleanSheet.Domain.Entities;
using CleanSheet.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CleanSheet.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CleanSheet.Domain.Entities.Career", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_update");

                    b.Property<string>("Manager")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("manager");

                    b.HasKey("Id")
                        .HasName("pk_careers");

                    b.ToTable("careers", (string)null);
                });

            modelBuilder.Entity("CleanSheet.Domain.Entities.InitialTeam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<IReadOnlyCollection<Player>>("Players")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("players");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("slug");

                    b.Property<string>("Stadium")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("stadium");

                    b.HasKey("Id")
                        .HasName("pk_initial_teams");

                    b.HasIndex("Slug")
                        .IsUnique()
                        .HasDatabaseName("ix_initial_teams_slug");

                    b.ToTable("initial_teams", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
