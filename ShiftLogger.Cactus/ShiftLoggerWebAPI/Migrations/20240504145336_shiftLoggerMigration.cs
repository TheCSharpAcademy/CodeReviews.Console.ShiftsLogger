using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftLogger.Cactus.Migrations
{
    /// <inheritdoc />
    public partial class shiftLoggerMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShiftLoggers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShiftDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ShiftEndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    TotalHoursWorked = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftLoggers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShiftLoggers");
        }
    }
}
