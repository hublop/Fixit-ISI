using Microsoft.EntityFrameworkCore.Migrations;

namespace Fixit.Persistance.Migrations
{
    public partial class AddCoordinatesToLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Location",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Location",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Location");
        }
    }
}
