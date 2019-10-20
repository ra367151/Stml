using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stml.Infrastructure.Datas.Migrations
{
    public partial class Update_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                schema: "dbo",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsEnable",
                schema: "dbo",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                schema: "dbo",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsEnable",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                schema: "dbo",
                table: "Users");
        }
    }
}
