using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftsLogger.SpyrosZoupas.Migrations
{
    /// <inheritdoc />
    public partial class DurationSecondsIsTableColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DurationSeconds",
                table: "Shifts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationSeconds",
                table: "Shifts");
        }
    }
}
