using Microsoft.EntityFrameworkCore.Migrations;

namespace Fixit.Persistance.Migrations
{
    public partial class AddDistributedOrdersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "DistributedOrderContractor",
                columns: table => new
                {
                    ContractorId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributedOrderContractor", x => new { x.ContractorId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_DistributedOrderContractor_AspNetUsers_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistributedOrderContractor_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistributedOrderContractor_OrderId",
                table: "DistributedOrderContractor",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistributedOrderContractor");

        }
    }
}
