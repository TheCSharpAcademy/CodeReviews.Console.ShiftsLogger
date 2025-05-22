using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftsLoggerV2.RyanW84.Data.Migrations
{
    /// <inheritdoc />
    public partial class AmendedAllModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "shiftId",
                table: "Workers");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Workers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_LocationId",
                table: "Workers",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Locations_LocationId",
                table: "Workers",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Locations_LocationId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_LocationId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Workers");

            migrationBuilder.AddColumn<int>(
                name: "shiftId",
                table: "Workers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
