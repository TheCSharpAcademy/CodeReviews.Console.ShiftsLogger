using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shiftsLogger.doc415.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Contacts",
                table: "Contacts");

            migrationBuilder.RenameTable(
                name: "Contacts",
                newName: "Shifts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shifts",
                table: "Shifts",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Shifts",
                table: "Shifts");

            migrationBuilder.RenameTable(
                name: "Shifts",
                newName: "Contacts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contacts",
                table: "Contacts",
                column: "Id");
        }
    }
}
