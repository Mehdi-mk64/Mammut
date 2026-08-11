using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class test12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                schema: "SMS",
                table: "MessageSend",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                schema: "SMS",
                table: "MessageSend",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEKv8FM88EAAgev80wY+gl376VRtAHtA8bvdc1zdd6cnFpIxGXpXfT0x63Z92dAUJpA==");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAECUimErfi9WVJYJaIw0OEKSOOE8hH/m8Bi6PUiePltB/yRLijzUddtEueTY6I76UFg==");
        }
    }
}
