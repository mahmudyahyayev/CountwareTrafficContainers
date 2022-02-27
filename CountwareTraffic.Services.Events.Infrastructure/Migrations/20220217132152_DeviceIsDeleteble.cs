using Microsoft.EntityFrameworkCore.Migrations;

namespace CountwareTraffic.Services.Events.Infrastructure.Migrations
{
    public partial class DeviceIsDeleteble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AuditIsDeleted",
                schema: "events",
                table: "Devices",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuditIsDeleted",
                schema: "events",
                table: "Devices");
        }
    }
}
