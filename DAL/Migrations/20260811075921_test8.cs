using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class test8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Security",
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEM4iSe/qOARx2eD6pKvhhQm3UWBqdcmDfG3kGcnO35f6/Lk23Z50qF5kkTnz+UgURw==");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEBHbTAxrCoQY1uFWBCCDS9kjxtCBb2WFcUW4zY42FnLL1b5L9dOCNLt6mv3rbQQ17Q==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Security",
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEFhFQxDo2BMc4jzlmt6pE5MHSryMox38bIViq4IWsBKyako5D+4/RYkEhMp3miIKQw==");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: null);
        }
    }
}
