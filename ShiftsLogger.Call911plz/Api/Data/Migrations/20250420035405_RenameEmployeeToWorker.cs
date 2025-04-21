using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameEmployeeToWorker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeName",
                table: "Workers",
                newName: "WorkerName");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Workers",
                newName: "WorkerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkerName",
                table: "Workers",
                newName: "EmployeeName");

            migrationBuilder.RenameColumn(
                name: "WorkerId",
                table: "Workers",
                newName: "EmployeeId");
        }
    }
}
