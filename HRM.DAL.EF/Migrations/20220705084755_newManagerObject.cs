using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM.DAL.EF.Migrations
{
    public partial class newManagerObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ManagerID",
                table: "Users",
                column: "ManagerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_ManagerID",
                table: "Users",
                column: "ManagerID",
                principalTable: "Users",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_ManagerID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ManagerID",
                table: "Users");

            migrationBuilder.AlterColumn<bool>(
                name: "Type",
                table: "Users",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
