using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftsLoggerAPI.Migrations
{
    /// <inheritdoc />
    public partial class workedHoursTypeChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WorkedHours",
                schema: "TCSA",
                table: "Shifts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "WorkedHours",
                schema: "TCSA",
                table: "Shifts",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)"
            );
        }
    }
}
