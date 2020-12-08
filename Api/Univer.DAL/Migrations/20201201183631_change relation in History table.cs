using Microsoft.EntityFrameworkCore.Migrations;

namespace Univer.DAL.Migrations
{
    public partial class changerelationinHistorytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersPublicData_History_HistoryId",
                table: "UsersPublicData");

            migrationBuilder.DropIndex(
                name: "IX_UsersPublicData_HistoryId",
                table: "UsersPublicData");

            migrationBuilder.DropColumn(
                name: "HistoryId",
                table: "UsersPublicData");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "History",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_History_UserId",
                table: "History",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_History_UsersPublicData_UserId",
                table: "History",
                column: "UserId",
                principalTable: "UsersPublicData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_UsersPublicData_UserId",
                table: "History");

            migrationBuilder.DropIndex(
                name: "IX_History_UserId",
                table: "History");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "History");

            migrationBuilder.AddColumn<int>(
                name: "HistoryId",
                table: "UsersPublicData",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersPublicData_HistoryId",
                table: "UsersPublicData",
                column: "HistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersPublicData_History_HistoryId",
                table: "UsersPublicData",
                column: "HistoryId",
                principalTable: "History",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
