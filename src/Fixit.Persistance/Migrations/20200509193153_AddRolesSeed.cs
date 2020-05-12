using Microsoft.EntityFrameworkCore.Migrations;

namespace Fixit.Persistance.Migrations
{
    public partial class AddRolesSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("GO\r\n\r\nINSERT INTO [dbo].[AspNetRoles]\r\n           ([Name]\r\n           ,[NormalizedName])\r\n     VALUES\r\n           ('Customer','Customer'),\r\n\t\t   ('ContractorUUID','ContractorUUID'),\r\n\t\t   ('Admin','Admin')\r\nGO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
