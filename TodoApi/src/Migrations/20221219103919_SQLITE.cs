﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Migrations
{
    public partial class SQLITE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "NVARCHAR", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "VARCHAR", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "NVARCHAR", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodoGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "NVARCHAR", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "VARCHAR", maxLength: 100, nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoGroup_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Todo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "NVARCHAR", maxLength: 200, nullable: false),
                    DeadLine = table.Column<DateTime>(type: "DATE", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "DATE", nullable: false, defaultValueSql: "DATE('now')"),
                    TodoGroupId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Todo_TodoGroup_TodoGroupId",
                        column: x => x.TodoGroupId,
                        principalTable: "TodoGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todo_TodoGroupId",
                table: "Todo",
                column: "TodoGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoGroup_UserId",
                table: "TodoGroup",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Slug",
                table: "User",
                column: "Slug",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todo");

            migrationBuilder.DropTable(
                name: "TodoGroup");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
