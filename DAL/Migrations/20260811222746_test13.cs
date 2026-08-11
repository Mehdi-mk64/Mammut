using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class test13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Security",
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAELDettaU5Kme+ZbVgKULFy6VVECc5TmQiKN+ATbVW1fivNXUQV3j+dsAmfdwJYFYWA==");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAENEWUXADh8n9F9Mct8P19Owit+eqqTkHHWe5oUjpBod9eczN7FuXTqVPikhcE7TXWQ==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Security",
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEP5XzLq2DYrP0WLR9PrrBwV8ZsUmPZCcTVnD81i/CpQLAvwpl2C1cHfgK0TSAdpU/A==");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEIc4QrHi95CMExlYvx48iwpidcJMwW9FnvE7eP8A84I32+IpZRNkytVnnLpvzfnJag==");
        }
    }
}
