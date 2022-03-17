using Microsoft.EntityFrameworkCore.Migrations;

namespace CountwareTraffic.Services.Events.Infrastructure.Migrations
{
    public partial class ChangeOnDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Devices_DeviceId",
                schema: "events",
                table: "Events");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Devices_DeviceId",
                schema: "events",
                table: "Events",
                column: "DeviceId",
                principalSchema: "events",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Devices_DeviceId",
                schema: "events",
                table: "Events");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Devices_DeviceId",
                schema: "events",
                table: "Events",
                column: "DeviceId",
                principalSchema: "events",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
