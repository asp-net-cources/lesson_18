using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lesson18.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoleStr",
                table: "Users",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("831627e6-e6ed-4754-b8f6-91be5cd221ec"),
                column: "RoleStr",
                value: "Admin");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Login", "Password", "RoleStr" },
                values: new object[] { new Guid("cda15a3f-38ea-4049-8d99-091ae31cc783"), "Пользователь", "Обычный", "user", "User12+", "Common" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cda15a3f-38ea-4049-8d99-091ae31cc783"));

            migrationBuilder.DropColumn(
                name: "RoleStr",
                table: "Users");
        }
    }
}
