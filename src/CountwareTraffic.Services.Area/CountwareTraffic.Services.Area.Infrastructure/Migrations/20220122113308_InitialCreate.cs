using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace CountwareTraffic.Services.Areas.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "areas");

            migrationBuilder.EnsureSchema(
                name: "area.app");

            migrationBuilder.CreateTable(
                name: "AreaTypes",
                schema: "areas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "areas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: true),
                    Address_Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address_Location = table.Column<Point>(type: "geography", nullable: true),
                    Contact_GsmNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Contact_PhoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Contact_EmailAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Contact_FaxNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditIsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AuditCreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                schema: "area.app",
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
                name: "SubAreaStatuses",
                schema: "areas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubAreaStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "areas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Iso = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Iso3 = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    IsoNumeric = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Capital = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContinentCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    CurrencyCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditIsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AuditCreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "areas",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "areas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditIsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AuditCreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "areas",
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                schema: "areas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditIsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AuditCreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Cities_CityId",
                        column: x => x.CityId,
                        principalSchema: "areas",
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                schema: "areas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: true),
                    AreaTypeId = table.Column<int>(type: "int", nullable: false),
                    DistrictId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_Location = table.Column<Point>(type: "geography", nullable: true),
                    Contact_GsmNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Contact_PhoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Contact_EmailAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Contact_FaxNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditIsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AuditCreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Areas_AreaTypes_AreaTypeId",
                        column: x => x.AreaTypeId,
                        principalSchema: "areas",
                        principalTable: "AreaTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Areas_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalSchema: "areas",
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubAreas",
                schema: "areas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: true),
                    AreaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubAreaStatus = table.Column<int>(type: "int", nullable: false),
                    AuditCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditCreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditIsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubAreas_Areas_AreaId",
                        column: x => x.AreaId,
                        principalSchema: "areas",
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubAreas_SubAreaStatuses_SubAreaStatus",
                        column: x => x.SubAreaStatus,
                        principalSchema: "areas",
                        principalTable: "SubAreaStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Areas_AreaTypeId",
                schema: "areas",
                table: "Areas",
                column: "AreaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_DistrictId",
                schema: "areas",
                table: "Areas",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                schema: "areas",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CompanyId",
                schema: "areas",
                table: "Countries",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_CityId",
                schema: "areas",
                table: "Districts",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_SubAreas_AreaId",
                schema: "areas",
                table: "SubAreas",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubAreas_SubAreaStatus",
                schema: "areas",
                table: "SubAreas",
                column: "SubAreaStatus");

            migrationBuilder.Sql(@"INSERT INTO [areas].[SubAreaStatuses] ( Id, Name ) VALUES
                                                       (1, 'Created'), (2, 'Completed'), (3, 'Rejected')");

            migrationBuilder.Sql(@"INSERT INTO [areas].[AreaTypes] ( Id, Name ) VALUES
                                                       (1, 'Unknown'), (2, 'Franchising'), (3, 'CompanyOwned')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "area.app");

            migrationBuilder.DropTable(
                name: "SubAreas",
                schema: "areas");

            migrationBuilder.DropTable(
                name: "Areas",
                schema: "areas");

            migrationBuilder.DropTable(
                name: "SubAreaStatuses",
                schema: "areas");

            migrationBuilder.DropTable(
                name: "AreaTypes",
                schema: "areas");

            migrationBuilder.DropTable(
                name: "Districts",
                schema: "areas");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "areas");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "areas");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "areas");
        }
    }
}
