using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CountwareTraffic.Services.Devices.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "devices");

            migrationBuilder.EnsureSchema(
                name: "device.app");

            migrationBuilder.CreateTable(
                name: "DeviceStatuses",
                schema: "devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceTypes",
                schema: "devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                schema: "device.app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OccurredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubAreas",
                schema: "devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                schema: "devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: true),
                    Model = table.Column<string>(type: "nvarchar(130)", maxLength: 130, nullable: true),
                    SubAreaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConnectionInfo_IpAddress = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ConnectionInfo_Port = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    ConnectionInfo_Identity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConnectionInfo_Password = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    ConnectionInfo_UniqueId = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    ConnectionInfo_MacAddress = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DeviceStatusId = table.Column<int>(type: "int", nullable: false),
                    DeviceTypeId = table.Column<int>(type: "int", nullable: false),
                    AuditCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditCreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditIsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_DeviceStatuses_DeviceStatusId",
                        column: x => x.DeviceStatusId,
                        principalSchema: "devices",
                        principalTable: "DeviceStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Devices_DeviceTypes_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalSchema: "devices",
                        principalTable: "DeviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Devices_SubAreas_SubAreaId",
                        column: x => x.SubAreaId,
                        principalSchema: "devices",
                        principalTable: "SubAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceStatusId",
                schema: "devices",
                table: "Devices",
                column: "DeviceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceTypeId",
                schema: "devices",
                table: "Devices",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_SubAreaId",
                schema: "devices",
                table: "Devices",
                column: "SubAreaId");

            migrationBuilder.Sql(@"INSERT INTO [devices].[DeviceStatuses] ( Id, Name ) VALUES
                                                       (1, 'Unknown'), (2, 'Connected'), (3, 'DisConnected'), (4, 'Broken')");

            migrationBuilder.Sql(@"INSERT INTO [devices].[DeviceTypes] ( Id, Name ) VALUES
                                                       (1, 'Unknown'), (2, 'Bio'), (3, 'AccessControl')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices",
                schema: "devices");

            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "device.app");

            migrationBuilder.DropTable(
                name: "DeviceStatuses",
                schema: "devices");

            migrationBuilder.DropTable(
                name: "DeviceTypes",
                schema: "devices");

            migrationBuilder.DropTable(
                name: "SubAreas",
                schema: "devices");
        }
    }
}
