using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftsLoggerV2.RyanW84.Data.Migrations
{
    /// <inheritdoc />
    public partial class AmendedWorkerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "shiftId",
                table: "Workers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "shiftId",
                table: "Workers");
        }
    }
}
