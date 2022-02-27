using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CountwareTraffic.WorkerServices.Sms.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "app");

            migrationBuilder.CreateTable(
                name: "SmsTypes",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmsLogs",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserIds = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: true),
                    PhoneNumbers = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: true),
                    SmsBody = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: true),
                    Response = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsOtp = table.Column<bool>(type: "bit", nullable: false),
                    SmsTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmsLogs_SmsTypes_SmsTypeId",
                        column: x => x.SmsTypeId,
                        principalSchema: "app",
                        principalTable: "SmsTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmsLogs_SmsTypeId",
                schema: "app",
                table: "SmsLogs",
                column: "SmsTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmsLogs",
                schema: "app");

            migrationBuilder.DropTable(
                name: "SmsTypes",
                schema: "app");
        }
    }
}
