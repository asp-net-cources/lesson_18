using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lesson18.Migrations
{
    /// <inheritdoc />
    public partial class AddDateOfBirth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("831627e6-e6ed-4754-b8f6-91be5cd221ec"),
                column: "DateOfBirth",
                value: new DateTime(2003, 10, 2, 11, 59, 36, 976, DateTimeKind.Local).AddTicks(7750));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cda15a3f-38ea-4049-8d99-091ae31cc783"),
                column: "DateOfBirth",
                value: new DateTime(2003, 10, 2, 11, 59, 36, 976, DateTimeKind.Local).AddTicks(7800));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "Login", "Password", "RoleStr" },
                values: new object[] { new Guid("36c24de2-e210-4da6-8e2e-e3c1683110cf"), new DateTime(2013, 10, 2, 11, 59, 36, 976, DateTimeKind.Local).AddTicks(7810), "Пользователь", "Молодой", "user-junior", "User12+", "Common" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("36c24de2-e210-4da6-8e2e-e3c1683110cf"));

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Users");
        }
    }
}
