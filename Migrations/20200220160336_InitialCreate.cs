using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BugReportModule.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BugReports",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    PlayerID = table.Column<int>(nullable: false),
                    BugDescription = table.Column<string>(nullable: true),
                    Logs = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BugReports", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BugReports");
        }
    }
}
