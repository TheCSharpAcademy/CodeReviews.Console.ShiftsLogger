using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftsLogger.weakiepedia.Migrations
{
    /// <inheritdoc />
    public partial class DurationInSecondsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DurationInSeconds",
                table: "Shifts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInSeconds",
                table: "Shifts");
        }
    }
}
