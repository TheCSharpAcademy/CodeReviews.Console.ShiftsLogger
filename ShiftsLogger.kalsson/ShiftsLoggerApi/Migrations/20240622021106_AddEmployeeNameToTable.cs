using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftsLoggerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeNameToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartShift",
                table: "ShiftModels",
                newName: "StartOfShift");

            migrationBuilder.RenameColumn(
                name: "EndShift",
                table: "ShiftModels",
                newName: "EndOfShift");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "ShiftModels",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "ShiftModels");

            migrationBuilder.RenameColumn(
                name: "StartOfShift",
                table: "ShiftModels",
                newName: "StartShift");

            migrationBuilder.RenameColumn(
                name: "EndOfShift",
                table: "ShiftModels",
                newName: "EndShift");
        }
    }
}
