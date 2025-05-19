using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftsLoggerV2.RyanW84.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedValidationToModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Workers",
                newName: "PhoneNumber");

            migrationBuilder.CreateTable(
                name: "LocationsWorkers",
                columns: table => new
                {
                    LocationsLocationId = table.Column<int>(type: "int", nullable: false),
                    WorkersWorkerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationsWorkers", x => new { x.LocationsLocationId, x.WorkersWorkerId });
                    table.ForeignKey(
                        name: "FK_LocationsWorkers_Locations_LocationsLocationId",
                        column: x => x.LocationsLocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationsWorkers_Workers_WorkersWorkerId",
                        column: x => x.WorkersWorkerId,
                        principalTable: "Workers",
                        principalColumn: "WorkerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationsWorkers_WorkersWorkerId",
                table: "LocationsWorkers",
                column: "WorkersWorkerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationsWorkers");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Workers",
                newName: "Phone");

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
    }
}
