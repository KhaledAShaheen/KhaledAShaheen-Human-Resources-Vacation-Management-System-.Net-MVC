using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM.DAL.EF.Migrations
{
    public partial class newtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Type",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Users");
        }
    }
}
