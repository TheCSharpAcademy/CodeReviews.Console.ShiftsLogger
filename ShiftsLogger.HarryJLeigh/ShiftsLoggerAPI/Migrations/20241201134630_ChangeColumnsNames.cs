using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftsLogger.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnsNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfTime",
                table: "Shifts");

            migrationBuilder.RenameColumn(
                name: "Hours",
                table: "Shifts",
                newName: "ShiftTime");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Shifts",
                newName: "StartTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Shifts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Shifts");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Shifts",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "ShiftTime",
                table: "Shifts",
                newName: "Hours");

            migrationBuilder.AddColumn<int>(
                name: "AmountOfTime",
                table: "Shifts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
