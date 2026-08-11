using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GSMSenderTitle",
                table: "ViewModelMessages");

            migrationBuilder.DropColumn(
                name: "TimeSend",
                table: "ViewModelMessages");

            migrationBuilder.RenameColumn(
                name: "DateSend",
                table: "ViewModelMessages",
                newName: "DateTimeSend");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTimeSend",
                table: "ViewModelMessages",
                newName: "DateSend");

            migrationBuilder.AddColumn<string>(
                name: "GSMSenderTitle",
                table: "ViewModelMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeSend",
                table: "ViewModelMessages",
                type: "time",
                nullable: true);
        }
    }
}
