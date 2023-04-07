﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpeedWebAPI.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpeedLimitPQA",
                columns: table => new
                {
                    Lat = table.Column<decimal>(type: "decimal(18,10)", precision: 18, scale: 10, nullable: false),
                    Lng = table.Column<decimal>(type: "decimal(18,10)", precision: 18, scale: 10, nullable: false),
                    ProviderType = table.Column<int>(type: "int", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MinSpeed = table.Column<int>(type: "int", nullable: true),
                    MaxSpeed = table.Column<int>(type: "int", nullable: true),
                    PointError = table.Column<bool>(type: "bit", nullable: true),
                    SegmentID = table.Column<long>(type: "bigint", nullable: true),
                    IsUpdateSpeed = table.Column<bool>(type: "bit", nullable: true),
                    Direction = table.Column<int>(type: "int", nullable: true),
                    STT = table.Column<int>(type: "int", nullable: false),
                    SpeedGPS = table.Column<int>(type: "int", nullable: false),
                    SpeedDetect = table.Column<int>(type: "int", nullable: false),
                    SpeedPQA = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RouteType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUpdSpeedPQA = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DeleteFlag = table.Column<int>(type: "int", nullable: true),
                    UpdateCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeedLimitPQA", x => new { x.Lat, x.Lng });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpeedLimitPQA");
        }
    }
}
