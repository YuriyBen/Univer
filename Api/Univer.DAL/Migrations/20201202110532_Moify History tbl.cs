using Microsoft.EntityFrameworkCore.Migrations;

namespace Univer.DAL.Migrations
{
    public partial class MoifyHistorytbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_UsersPublicData_UserId",
                table: "History");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "History",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_History_UsersPublicData_UserId",
                table: "History",
                column: "UserId",
                principalTable: "UsersPublicData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_UsersPublicData_UserId",
                table: "History");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "History",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_History_UsersPublicData_UserId",
                table: "History",
                column: "UserId",
                principalTable: "UsersPublicData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
