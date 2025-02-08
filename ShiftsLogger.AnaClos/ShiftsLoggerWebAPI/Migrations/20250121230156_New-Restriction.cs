using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftsLoggerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewRestriction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Employee_EmployeeId",
                table: "Shift");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employee",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Name",
                table: "Employee",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_Employee_EmployeeId",
                table: "Shift",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Employee_EmployeeId",
                table: "Shift");

            migrationBuilder.DropIndex(
                name: "IX_Employee_Name",
                table: "Employee");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_Employee_EmployeeId",
                table: "Shift",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
