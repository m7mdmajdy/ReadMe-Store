﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booky_Store.Data.Migrations
{
    public partial class IgonreUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "Security",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "Security",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
