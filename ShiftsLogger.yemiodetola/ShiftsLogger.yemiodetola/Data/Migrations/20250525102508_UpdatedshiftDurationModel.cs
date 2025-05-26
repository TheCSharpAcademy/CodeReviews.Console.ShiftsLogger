using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShfitsLogger.yemiodetola.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedshiftDurationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Shifts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Duration",
                table: "Shifts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
