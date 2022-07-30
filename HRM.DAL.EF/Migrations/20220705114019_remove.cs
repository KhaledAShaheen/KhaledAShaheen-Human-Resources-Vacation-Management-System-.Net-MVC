using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM.DAL.EF.Migrations
{
    public partial class remove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_ManagerID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ManagerID",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
