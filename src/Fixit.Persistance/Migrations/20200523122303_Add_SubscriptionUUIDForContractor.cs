using Microsoft.EntityFrameworkCore.Migrations;

namespace Fixit.Persistance.Migrations
{
    public partial class Add_SubscriptionUUIDForContractor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubscriptionUUID",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionUUID",
                table: "AspNetUsers");
        }
    }
}
