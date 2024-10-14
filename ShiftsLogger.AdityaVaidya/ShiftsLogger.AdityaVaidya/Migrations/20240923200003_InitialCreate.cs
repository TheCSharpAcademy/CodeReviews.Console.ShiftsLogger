using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftsLogger.AdityaVaidya.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Logic for applying changes to the database should go here.
            // For example: creating tables, adding columns, etc.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // This method is intentionally left empty because no rollback logic is required
            // for this migration. Typically, this would drop the changes made in the Up method,
            // such as removing tables or columns created.
        }
    }
}
