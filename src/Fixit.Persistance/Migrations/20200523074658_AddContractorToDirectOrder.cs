using Microsoft.EntityFrameworkCore.Migrations;

namespace Fixit.Persistance.Migrations
{
    public partial class AddContractorToDirectOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractorId",
                table: "Order",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_ContractorId",
                table: "Order",
                column: "ContractorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_ContractorId",
                table: "Order",
                column: "ContractorId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_ContractorId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ContractorId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ContractorId",
                table: "Order");
        }
    }
}
