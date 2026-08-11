using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class test11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageSend_ApplicationUser_ApplicationUserId",
                schema: "SMS",
                table: "MessageSend");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                schema: "SMS",
                table: "MessageSend",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_MessageSend_ApplicationUserId",
                schema: "SMS",
                table: "MessageSend",
                newName: "IX_MessageSend_UserID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_MessageSend_ApplicationUser_ID",
                schema: "SMS",
                table: "MessageSend",
                column: "UserID",
                principalSchema: "Security",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageSend_ApplicationUser_ID",
                schema: "SMS",
                table: "MessageSend");

            migrationBuilder.RenameColumn(
                name: "UserID",
                schema: "SMS",
                table: "MessageSend",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageSend_UserID",
                schema: "SMS",
                table: "MessageSend",
                newName: "IX_MessageSend_ApplicationUserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_MessageSend_ApplicationUser_ApplicationUserId",
                schema: "SMS",
                table: "MessageSend",
                column: "ApplicationUserId",
                principalSchema: "Security",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
