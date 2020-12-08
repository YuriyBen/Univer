using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Univer.DAL.Migrations
{
    public partial class AddHistorytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "HistoryId",
                table: "UsersPublicData",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MatrixSizes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Result = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersPublicData_History_HistoryId",
                table: "UsersPublicData");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropIndex(
                name: "IX_UsersPublicData_HistoryId",
                table: "UsersPublicData");

            migrationBuilder.DropColumn(
                name: "HistoryId",
                table: "UsersPublicData");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
