using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShiftLogger.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedShiftData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Shifts",
                columns: new[] { "Id", "EmployeeName", "End", "Start" },
                values: new object[,]
                {
                    { 1, "Adam", new DateTime(2024, 9, 26, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 26, 12, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Eve", new DateTime(2024, 9, 26, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 26, 12, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
