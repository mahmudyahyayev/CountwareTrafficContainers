using Microsoft.EntityFrameworkCore.Migrations;

namespace CountwareTraffic.Services.Devices.Infrastructure.Migrations
{
    public partial class AddDeviceCreationStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceCreationStatus",
                schema: "devices",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DeviceCreationStatuses",
                schema: "devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCreationStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceCreationStatus",
                schema: "devices",
                table: "Devices",
                column: "DeviceCreationStatus");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceCreationStatuses_DeviceCreationStatus",
                schema: "devices",
                table: "Devices",
                column: "DeviceCreationStatus",
                principalSchema: "devices",
                principalTable: "DeviceCreationStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.Sql(@"INSERT INTO [devices].[DeviceCreationStatuses] ( Id, Name ) VALUES
                                                       (1, 'Created'), (2, 'Completed'), (3, 'Rejected')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceCreationStatuses_DeviceCreationStatus",
                schema: "devices",
                table: "Devices");

            migrationBuilder.DropTable(
                name: "DeviceCreationStatuses",
                schema: "devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_DeviceCreationStatus",
                schema: "devices",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "DeviceCreationStatus",
                schema: "devices",
                table: "Devices");
        }
    }
}
