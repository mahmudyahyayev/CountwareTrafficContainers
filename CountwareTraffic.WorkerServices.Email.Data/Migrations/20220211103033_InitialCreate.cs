using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CountwareTraffic.WorkerServices.Email.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "app");

            migrationBuilder.CreateTable(
                name: "EmailTypes",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailLogs",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserIds = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EmailSubject = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EmailBody = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: true),
                    EmailTo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Response = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsHtml = table.Column<bool>(type: "bit", nullable: false),
                    EmailTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailLogs_EmailTypes_EmailTypeId",
                        column: x => x.EmailTypeId,
                        principalSchema: "app",
                        principalTable: "EmailTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailLogs_EmailTypeId",
                schema: "app",
                table: "EmailLogs",
                column: "EmailTypeId");

            migrationBuilder.Sql(@"INSERT INTO [app].[EmailTypes] ( Id, Name ) VALUES
                                                       (1, 'Default'), (2, 'Templated')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailLogs",
                schema: "app");

            migrationBuilder.DropTable(
                name: "EmailTypes",
                schema: "app");
        }
    }
}
