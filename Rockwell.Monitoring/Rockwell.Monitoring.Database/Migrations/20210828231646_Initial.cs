using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace Rockwell.Monitoring.Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "scrape-jobs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Url = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    CronExpression = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scrape-jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "execution-results",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ExecutionTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    ScrapeJobId = table.Column<long>(type: "bigint", nullable: false),
                    ResponseCode = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    ErrorMessage = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_execution-results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_execution-results_scrape-jobs_ScrapeJobId",
                        column: x => x.ScrapeJobId,
                        principalTable: "scrape-jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_execution-results_ScrapeJobId",
                table: "execution-results",
                column: "ScrapeJobId");

            migrationBuilder.CreateIndex(
                name: "IX_scrape-jobs_Url_CronExpression",
                table: "scrape-jobs",
                columns: new[] { "Url", "CronExpression" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "execution-results");

            migrationBuilder.DropTable(
                name: "scrape-jobs");
        }
    }
}
