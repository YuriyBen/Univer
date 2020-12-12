using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Univer.DAL.Migrations
{
    public partial class Addphoneverification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WantsToRecieveNotifications",
                table: "UsersPublicData");

            migrationBuilder.AddColumn<long>(
                name: "SecretKey",
                table: "UsersPublicData",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "SecretKeyValidTo",
                table: "UsersPublicData",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecretKey",
                table: "UsersPublicData");

            migrationBuilder.DropColumn(
                name: "SecretKeyValidTo",
                table: "UsersPublicData");

            migrationBuilder.AddColumn<bool>(
                name: "WantsToRecieveNotifications",
                table: "UsersPublicData",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
