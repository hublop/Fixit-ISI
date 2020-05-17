using Microsoft.EntityFrameworkCore.Migrations;

namespace Fixit.Persistance.Migrations
{
    public partial class RoleAndSubscriptionSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          migrationBuilder.Sql("GO\r\n\r\nINSERT INTO [dbo].[AspNetRoles]\r\n           ([Name]\r\n           ,[NormalizedName])\r\n     VALUES\r\n           ('Customer','Customer'),\r\n\t\t   ('Contractor','Contractor'),\r\n\t\t   ('Admin','Admin')\r\nGO");
          migrationBuilder.Sql("GO\r\n\r\nINSERT INTO [dbo].[SubscriptionStatus]\r\n           ([SubscriptionStatusId],[Status])\r\n     VALUES\r\n           (10,'Active'),(20,'Cancelled'),(30,'Deactivated')\r\nGO\r\n");
    }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
