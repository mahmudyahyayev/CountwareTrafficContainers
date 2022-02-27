using Microsoft.EntityFrameworkCore.Migrations;

namespace CountwareTraffic.Services.Devices.Infrastructure.Migrations
{
    public partial class ChangeOnDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_SubAreas_SubAreaId",
                schema: "devices",
                table: "Devices");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_SubAreas_SubAreaId",
                schema: "devices",
                table: "Devices",
                column: "SubAreaId",
                principalSchema: "devices",
                principalTable: "SubAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_SubAreas_SubAreaId",
                schema: "devices",
                table: "Devices");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_SubAreas_SubAreaId",
                schema: "devices",
                table: "Devices",
                column: "SubAreaId",
                principalSchema: "devices",
                principalTable: "SubAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
