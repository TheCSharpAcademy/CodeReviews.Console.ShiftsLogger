using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftsLogger.Ryanw84.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    WorkerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    ShiftId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.ShiftId);
                    table.ForeignKey(
                        name: "FK_Shift_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Worker",
                columns: table => new
                {
                    WorkerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker", x => x.WorkerId);
                    table.ForeignKey(
                        name: "FK_Worker_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "ShiftId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Location_ShiftId",
                table: "Location",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_WorkerId",
                table: "Location",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_LocationId",
                table: "Shift",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_WorkerId",
                table: "Shift",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_ShiftId",
                table: "Worker",
                column: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Shift_ShiftId",
                table: "Location",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Worker_WorkerId",
                table: "Location",
                column: "WorkerId",
                principalTable: "Worker",
                principalColumn: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_Worker_WorkerId",
                table: "Shift",
                column: "WorkerId",
                principalTable: "Worker",
                principalColumn: "WorkerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Shift_ShiftId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Worker_Shift_ShiftId",
                table: "Worker");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Worker");
        }
    }
}
