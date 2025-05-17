using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftsLoggerAPI.Migrations
{
    /// <inheritdoc />
    public partial class schemaChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_ShiftTypes_ShiftTypeId",
                schema: "TCSA",
                table: "Shifts"
            );

            migrationBuilder.DropTable(name: "ShiftTypes", schema: "TCSA");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_ShiftTypeId",
                schema: "TCSA",
                table: "Shifts"
            );

            migrationBuilder.DropColumn(name: "ShiftTypeId", schema: "TCSA", table: "Shifts");

            migrationBuilder.AddColumn<string>(
                name: "ShiftType",
                schema: "TCSA",
                table: "Shifts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: ""
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "ShiftType", schema: "TCSA", table: "Shifts");

            migrationBuilder.AddColumn<int>(
                name: "ShiftTypeId",
                schema: "TCSA",
                table: "Shifts",
                type: "int",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.CreateTable(
                name: "ShiftTypes",
                schema: "TCSA",
                columns: table => new
                {
                    ShiftTypeId = table
                        .Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftTypes", x => x.ShiftTypeId);
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_ShiftTypeId",
                schema: "TCSA",
                table: "Shifts",
                column: "ShiftTypeId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_ShiftTypes_ShiftTypeId",
                schema: "TCSA",
                table: "Shifts",
                column: "ShiftTypeId",
                principalSchema: "TCSA",
                principalTable: "ShiftTypes",
                principalColumn: "ShiftTypeId",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
