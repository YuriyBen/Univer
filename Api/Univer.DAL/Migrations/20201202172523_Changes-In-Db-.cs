using Microsoft.EntityFrameworkCore.Migrations;

namespace Univer.DAL.Migrations
{
    public partial class ChangesInDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Result",
                table: "History",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "History",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrentlyExecuted",
                table: "History",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "History");

            migrationBuilder.DropColumn(
                name: "IsCurrentlyExecuted",
                table: "History");

            migrationBuilder.AlterColumn<int>(
                name: "Result",
                table: "History",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
