using Microsoft.EntityFrameworkCore.Migrations;

namespace Univer.DAL.Migrations
{
    public partial class AddResultMatrixtodb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Result",
                table: "History",
                newName: "MatrixSum");

            migrationBuilder.AddColumn<string>(
                name: "ResultMatrix",
                table: "History",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultMatrix",
                table: "History");

            migrationBuilder.RenameColumn(
                name: "MatrixSum",
                table: "History",
                newName: "Result");
        }
    }
}
