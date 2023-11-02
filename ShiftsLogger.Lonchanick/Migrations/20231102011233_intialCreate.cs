using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShiftsLogger.Lonchanick.Migrations
{
    /// <inheritdoc />
    public partial class intialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Worker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Check = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckTypeField = table.Column<int>(type: "int", nullable: false),
                    WorkerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shift_Worker_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Worker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Worker",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2928fb74-46c1-439c-b8ad-b9aee833fa02"), "Ramon" },
                    { new Guid("2928fb74-46c1-439c-b8ad-b9aee833fa03"), "Trespatines" },
                    { new Guid("2928fb74-46c1-439c-b8ad-b9aee833fa97"), "Leopoldo" }
                });

            migrationBuilder.InsertData(
                table: "Shift",
                columns: new[] { "Id", "Check", "CheckTypeField", "WorkerId" },
                values: new object[,]
                {
                    { new Guid("85df9217-bc1a-4490-92c9-883b572bc001"), new DateTime(2023, 11, 1, 22, 12, 33, 401, DateTimeKind.Local).AddTicks(7783), 0, new Guid("2928fb74-46c1-439c-b8ad-b9aee833fa97") },
                    { new Guid("85df9217-bc1a-4490-92c9-883b572bc002"), new DateTime(2023, 11, 1, 22, 12, 33, 401, DateTimeKind.Local).AddTicks(7820), 0, new Guid("2928fb74-46c1-439c-b8ad-b9aee833fa02") },
                    { new Guid("85df9217-bc1a-4490-92c9-883b572bc003"), new DateTime(2023, 11, 1, 22, 12, 33, 401, DateTimeKind.Local).AddTicks(7825), 0, new Guid("2928fb74-46c1-439c-b8ad-b9aee833fa03") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shift_WorkerId",
                table: "Shift",
                column: "WorkerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropTable(
                name: "Worker");
        }
    }
}
