using Microsoft.EntityFrameworkCore.Migrations;

namespace CountwareTraffic.Services.Devices.Infrastructure.Migrations
{
    public partial class SubareaIsDeleteble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AuditIsDeleted",
                schema: "devices",
                table: "SubAreas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuditIsDeleted",
                schema: "devices",
                table: "SubAreas");
        }
    }
}
