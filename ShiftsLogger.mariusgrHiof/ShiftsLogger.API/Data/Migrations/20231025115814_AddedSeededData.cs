using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftsLogger.API.Data.Migrations
{
    public partial class AddedSeededData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Workers",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[] { 1, "Marius", "Gravningsmyhr" });

            migrationBuilder.InsertData(
                table: "Workers",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[] { 2, "Ola", "Nordmann" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
