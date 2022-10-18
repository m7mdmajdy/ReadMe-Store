using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booky_Store.Data.Migrations
{
    public partial class AddAdminRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [Security].[UserRoles] (UserId,RoleId) SELECT '2238b960-b302-4030-9504-d25f9611b6ae', Id FROM [Security].[Roles]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Security].[UserRoles] WHERE UserId='2238b960-b302-4030-9504-d25f9611b6ae'");
        }
    }
}
