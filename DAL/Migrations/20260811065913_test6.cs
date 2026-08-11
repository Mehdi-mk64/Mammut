using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class test6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "HR",
                table: "Person",
                columns: new[] { "ID", "FirstName", "GenderID", "IsActive", "LastName", "PersonCode" },
                values: new object[] { 2L, "admin", (byte)2, true, "admin", "1" });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "ApplicationUser",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonID", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "STATIC-ADMIN-CONCURRENCY-STAMP", "admin@mammut.local", true, false, null, "ADMIN@MAMMUT.LOCAL", "ADMIN", "AQAAAAEAACcQAAAAEGT/TsslZcQ9e99j5r8xDCzCyZHDZawLYXDzuYqM8sC9wyb+ouK1h45UOe0hJabIPw==", 2L, null, false, "STATIC-ADMIN-SECURITY-STAMP", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Security",
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "HR",
                table: "Person",
                keyColumn: "ID",
                keyValue: 2L);
        }
    }
}
