using System;
using System.Collections.Generic;
using CleanSheet.Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanSheet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialTeamsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                table: "careers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "initial_teams",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    stadium = table.Column<string>(type: "text", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: false),
                    players = table.Column<IReadOnlyCollection<Player>>(type: "jsonb", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_initial_teams", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_initial_teams_slug",
                table: "initial_teams",
                column: "slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "initial_teams");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                table: "careers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
