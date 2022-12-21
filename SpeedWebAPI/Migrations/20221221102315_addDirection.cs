using Microsoft.EntityFrameworkCore.Migrations;

namespace SpeedWebAPI.Migrations
{
    public partial class addDirection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Direction",
                table: "SpeedLimit",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Direction",
                table: "SpeedLimit");
        }
    }
}
