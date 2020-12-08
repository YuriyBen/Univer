using Microsoft.EntityFrameworkCore.Migrations;

namespace Univer.DAL.Migrations
{
    public partial class ChangesInDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_UsersPublicData_UserId",
                table: "History");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "History",
                newName: "UserPublicDataId");

            migrationBuilder.RenameIndex(
                name: "IX_History_UserId",
                table: "History",
                newName: "IX_History_UserPublicDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_History_UsersPublicData_UserPublicDataId",
                table: "History",
                column: "UserPublicDataId",
                principalTable: "UsersPublicData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_UsersPublicData_UserPublicDataId",
                table: "History");

            migrationBuilder.RenameColumn(
                name: "UserPublicDataId",
                table: "History",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_History_UserPublicDataId",
                table: "History",
                newName: "IX_History_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_History_UsersPublicData_UserId",
                table: "History",
                column: "UserId",
                principalTable: "UsersPublicData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
