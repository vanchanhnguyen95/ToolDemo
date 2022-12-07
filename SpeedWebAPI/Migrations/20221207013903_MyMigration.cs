using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpeedWebAPI.Migrations
{
    public partial class MyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpeedLimit",
                columns: table => new
                {
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lng = table.Column<double>(type: "float", nullable: false),
                    ProviderType = table.Column<int>(type: "int", maxLength: 1, nullable: false),
                    MinSpeed = table.Column<int>(type: "int", nullable: true),
                    MaxSpeed = table.Column<int>(type: "int", nullable: true),
                    PointError = table.Column<bool>(type: "bit", nullable: true),
                    SegmentID = table.Column<long>(type: "bigint", nullable: true),
                    Position = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DeleteFlag = table.Column<int>(type: "int", nullable: true),
                    UpdateCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeedLimit", x => new { x.Lat, x.Lng, x.ProviderType });
                });

            migrationBuilder.CreateTable(
                name: "SpeedLimit3Point",
                columns: table => new
                {
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lng = table.Column<double>(type: "float", nullable: false),
                    ProviderType = table.Column<int>(type: "int", maxLength: 1, nullable: false),
                    MinSpeed = table.Column<int>(type: "int", nullable: true),
                    MaxSpeed = table.Column<int>(type: "int", nullable: true),
                    PointError = table.Column<bool>(type: "bit", nullable: true),
                    SegmentID = table.Column<long>(type: "bigint", nullable: true),
                    Position = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DeleteFlag = table.Column<int>(type: "int", nullable: true),
                    UpdateCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeedLimit3Point", x => new { x.Lat, x.Lng, x.ProviderType });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpeedLimit");

            migrationBuilder.DropTable(
                name: "SpeedLimit3Point");
        }
    }
}
