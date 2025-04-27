using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftsLogger.SpyrosZoupas.Migrations
{
    /// <inheritdoc />
    public partial class DurationSecondsTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationSeconds",
                table: "Shifts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DurationSeconds",
                table: "Shifts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
