using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Buutyful.ShiftsLogger.Api.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Shifts",
                columns: new[] { "Id", "Duration", "EndAt", "ShiftDay", "StartAt", "WorkerId" },
                values: new object[] { new Guid("245645c5-baeb-4196-b635-5ebfd418a4a8"), new TimeSpan(-288000000002), new DateTime(2024, 2, 26, 0, 4, 4, 877, DateTimeKind.Local).AddTicks(1267), new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 2, 25, 16, 4, 4, 877, DateTimeKind.Local).AddTicks(1265), new Guid("6ce80c50-da19-4035-9e7f-3061f20a17e0") });

            migrationBuilder.InsertData(
                table: "Workers",
                columns: new[] { "Id", "Name", "Role" },
                values: new object[,]
                {
                    { new Guid("1d2fd51b-5537-489b-98c5-4c5095b832e2"), "Worker2", 1 },
                    { new Guid("6ce80c50-da19-4035-9e7f-3061f20a17e0"), "Worker1", 0 },
                    { new Guid("cf037397-311d-4574-a663-05e1af317da8"), "Worker3", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Workers");
        }
    }
}
