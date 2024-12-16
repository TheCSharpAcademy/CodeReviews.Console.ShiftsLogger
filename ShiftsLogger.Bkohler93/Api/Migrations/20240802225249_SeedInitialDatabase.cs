using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Shifts",
                columns: new[] { "Id", "EndTime", "Name", "StartTime" },
                values: new object[,]
                {
                    { 1, new TimeOnly(13, 0, 0), "Morning Shift", new TimeOnly(9, 0, 0) },
                    { 2, new TimeOnly(17, 0, 0), "Afternoon Shift", new TimeOnly(13, 0, 0) },
                    { 3, new TimeOnly(21, 0, 0), "Evening Shift", new TimeOnly(17, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Workers",
                columns: new[] { "Id", "FirstName", "LastName", "Position" },
                values: new object[,]
                {
                    { 1, "John", "Doe", "Manager" },
                    { 2, "Jane", "Smith", "Developer" },
                    { 3, "Emily", "Johnson", "Designer" }
                });

            migrationBuilder.InsertData(
                table: "WorkerShifts",
                columns: new[] { "Id", "ShiftDate", "ShiftId", "WorkerId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 8, 1), 1, 1 },
                    { 2, new DateOnly(2024, 8, 2), 2, 1 },
                    { 3, new DateOnly(2024, 8, 1), 1, 2 },
                    { 4, new DateOnly(2024, 8, 2), 3, 2 },
                    { 5, new DateOnly(2024, 8, 1), 2, 3 },
                    { 6, new DateOnly(2024, 8, 2), 3, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorkerShifts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkerShifts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WorkerShifts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WorkerShifts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "WorkerShifts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "WorkerShifts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
