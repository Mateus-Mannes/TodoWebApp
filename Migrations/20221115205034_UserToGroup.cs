using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Migrations
{
    public partial class UserToGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todo_User_UserId",
                table: "Todo");

            migrationBuilder.DropIndex(
                name: "IX_Todo_UserId",
                table: "Todo");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Todo");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TodoGroup",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TodoGroup_UserId",
                table: "TodoGroup",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoGroup_User_UserId",
                table: "TodoGroup",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoGroup_User_UserId",
                table: "TodoGroup");

            migrationBuilder.DropIndex(
                name: "IX_TodoGroup_UserId",
                table: "TodoGroup");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TodoGroup");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Todo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Todo_UserId",
                table: "Todo",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todo_User_UserId",
                table: "Todo",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
