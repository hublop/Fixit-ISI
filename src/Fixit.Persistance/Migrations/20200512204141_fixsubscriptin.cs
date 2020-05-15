using Microsoft.EntityFrameworkCore.Migrations;

namespace Fixit.Persistance.Migrations
{
    public partial class fixsubscriptin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          migrationBuilder.Sql("GO\r\n\r\nINSERT INTO [dbo].[SubscriptionStatus]\r\n           ([SubscriptionStatusId],[Status])\r\n     VALUES\r\n           (10,'Active'),(20,'Cancelled'),(30,'Deactivated')\r\nGO\r\n");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
