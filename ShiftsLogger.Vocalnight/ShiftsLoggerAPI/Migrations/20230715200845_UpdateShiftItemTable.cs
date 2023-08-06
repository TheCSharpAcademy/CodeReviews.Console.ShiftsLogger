using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftsLogger.Migrations
{
    /// <inheritdoc />
    public partial class UpdateShiftItemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShiftItem_Employees_EmployeeId",
                table: "ShiftItem");

            migrationBuilder.DropIndex(
                name: "IX_ShiftItem_EmployeeId",
                table: "ShiftItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ShiftItem_EmployeeId",
                table: "ShiftItem",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftItem_Employees_EmployeeId",
                table: "ShiftItem",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
