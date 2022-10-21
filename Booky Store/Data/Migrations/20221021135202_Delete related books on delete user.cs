using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booky_Store.Data.Migrations
{
    public partial class Deleterelatedbooksondeleteuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_ApplicationUserId",
                schema: "Security",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                schema: "Security",
                table: "Books",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_ApplicationUserId",
                schema: "Security",
                table: "Books",
                column: "ApplicationUserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_ApplicationUserId",
                schema: "Security",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                schema: "Security",
                table: "Books",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_ApplicationUserId",
                schema: "Security",
                table: "Books",
                column: "ApplicationUserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
