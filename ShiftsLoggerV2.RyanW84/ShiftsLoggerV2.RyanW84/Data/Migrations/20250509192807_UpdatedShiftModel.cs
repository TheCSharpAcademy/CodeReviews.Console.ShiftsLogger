using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftsLoggerV2.RyanW84.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedShiftModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedTo",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Shifts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Shifts",
                newName: "ShiftId");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Shifts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "workerId",
                table: "Shifts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "workerId",
                table: "Shifts");

            migrationBuilder.RenameColumn(
                name: "ShiftId",
                table: "Shifts",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "AssignedTo",
                table: "Shifts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Shifts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Shifts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Shifts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Shifts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
