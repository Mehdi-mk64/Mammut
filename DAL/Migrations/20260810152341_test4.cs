using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class test4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageSend_PhoneNumber_ID",
                schema: "SMS",
                table: "MessageSend");

            migrationBuilder.DropIndex(
                name: "IX_MessageSend_PhoneNumberID",
                schema: "SMS",
                table: "MessageSend");

            migrationBuilder.DropColumn(
                name: "PhoneNumberID",
                schema: "SMS",
                table: "MessageSend");

            migrationBuilder.AddColumn<long>(
                name: "PhonNumbersID",
                schema: "SMS",
                table: "MessageSend",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessageSend_PhonNumbersID",
                schema: "SMS",
                table: "MessageSend",
                column: "PhonNumbersID");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageSend_PhonNumbers_PhonNumbersID",
                schema: "SMS",
                table: "MessageSend",
                column: "PhonNumbersID",
                principalSchema: "HR",
                principalTable: "PhonNumbers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageSend_PhonNumbers_PhonNumbersID",
                schema: "SMS",
                table: "MessageSend");

            migrationBuilder.DropIndex(
                name: "IX_MessageSend_PhonNumbersID",
                schema: "SMS",
                table: "MessageSend");

            migrationBuilder.DropColumn(
                name: "PhonNumbersID",
                schema: "SMS",
                table: "MessageSend");

            migrationBuilder.AddColumn<long>(
                name: "PhoneNumberID",
                schema: "SMS",
                table: "MessageSend",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_MessageSend_PhoneNumberID",
                schema: "SMS",
                table: "MessageSend",
                column: "PhoneNumberID");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageSend_PhoneNumber_ID",
                schema: "SMS",
                table: "MessageSend",
                column: "PhoneNumberID",
                principalSchema: "HR",
                principalTable: "PhonNumbers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
