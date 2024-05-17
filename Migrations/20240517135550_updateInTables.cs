using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace callibrus.server.Migrations
{
    public partial class updateInTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Bookings",
                newName: "UserName");

            migrationBuilder.AddColumn<string>(
                name: "AvailableCopies",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeathDate",
                table: "Authors",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableCopies",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DeathDate",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Bookings",
                newName: "Status");
        }
    }
}
