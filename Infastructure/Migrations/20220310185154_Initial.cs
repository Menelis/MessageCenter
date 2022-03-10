using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RunningYears",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunningYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BirthDaySentLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    RunningYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateSent = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirthDaySentLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BirthDaySentLogs_RunningYears_RunningYearId",
                        column: x => x.RunningYearId,
                        principalTable: "RunningYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BirthDaySentLogs_RunningYearId",
                table: "BirthDaySentLogs",
                column: "RunningYearId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BirthDaySentLogs");

            migrationBuilder.DropTable(
                name: "RunningYears");
        }
    }
}
