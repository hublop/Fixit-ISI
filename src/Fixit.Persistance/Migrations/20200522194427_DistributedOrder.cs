using Microsoft.EntityFrameworkCore.Migrations;

namespace Fixit.Persistance.Migrations
{
    public partial class DistributedOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDistributed",
                table: "Order",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDistributed",
                table: "Order");
        }
    }
}
