using Microsoft.EntityFrameworkCore.Migrations;

namespace Stml.Infrastructure.Datas.Migrations
{
    public partial class Add_RolePermission_Obsolete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Permission_Obsolete",
                schema: "dbo",
                table: "RolePermissions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permission_Obsolete",
                schema: "dbo",
                table: "RolePermissions");
        }
    }
}
