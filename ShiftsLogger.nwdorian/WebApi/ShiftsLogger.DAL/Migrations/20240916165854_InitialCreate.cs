using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftsLogger.DAL.Migrations
{
	/// <inheritdoc />
	public partial class InitialCreate : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
					name: "Shift",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
						EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
						IsActive = table.Column<bool>(type: "bit", nullable: false),
						DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
						DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Shift", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "User",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
						LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
						Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
						IsActive = table.Column<bool>(type: "bit", nullable: false),
						DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
						DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_User", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "UserShift",
					columns: table => new
					{
						ShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_UserShift", x => new { x.ShiftId, x.UserId });
						table.ForeignKey(
											name: "FK_UserShift_Shift_ShiftId",
											column: x => x.ShiftId,
											principalTable: "Shift",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_UserShift_User_UserId",
											column: x => x.UserId,
											principalTable: "User",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateIndex(
					name: "IX_UserShift_UserId",
					table: "UserShift",
					column: "UserId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
					name: "UserShift");

			migrationBuilder.DropTable(
					name: "Shift");

			migrationBuilder.DropTable(
					name: "User");
		}
	}
}
