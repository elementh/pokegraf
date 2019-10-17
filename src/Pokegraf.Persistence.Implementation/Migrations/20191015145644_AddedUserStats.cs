using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokegraf.Persistence.Implementation.Migrations
{
    public partial class AddedUserStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StatsId",
                table: "Users",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatsRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Pokemon = table.Column<int>(nullable: false),
                    Fusion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatsRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatsRequests_Stats_Id",
                        column: x => x.Id,
                        principalTable: "Stats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_StatsId",
                table: "Users",
                column: "StatsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Stats_StatsId",
                table: "Users",
                column: "StatsId",
                principalTable: "Stats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Stats_StatsId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "StatsRequests");

            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropIndex(
                name: "IX_Users_StatsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StatsId",
                table: "Users");
        }
    }
}
