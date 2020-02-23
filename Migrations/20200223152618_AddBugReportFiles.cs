using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BugReportModule.Migrations
{
    public partial class AddBugReportFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BugReportFiles",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BugReportID = table.Column<Guid>(nullable: false),
                    Filename = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BugReportFiles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BugReportFiles_BugReports_BugReportID",
                        column: x => x.BugReportID,
                        principalTable: "BugReports",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BugReportFiles_BugReportID",
                table: "BugReportFiles",
                column: "BugReportID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BugReportFiles");
        }
    }
}
