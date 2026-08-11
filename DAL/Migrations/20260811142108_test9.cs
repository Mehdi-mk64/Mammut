using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class test9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProviderMessageID",
                schema: "SMS",
                table: "MessageLog",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEOB9cqpZ2uzxHthRwymmcd1fxVwV0x94HPF4w/cyRubo/VLda3r4MEYhHTjZJjnizA==");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEFjMsYk78QaeKsT7NE1gCtQLQImz3dkAgO4cbwncWrnMpZeKWjywbpPYUV0Wp3LfDw==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProviderMessageID",
                schema: "SMS",
                table: "MessageLog");

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
    }
}
