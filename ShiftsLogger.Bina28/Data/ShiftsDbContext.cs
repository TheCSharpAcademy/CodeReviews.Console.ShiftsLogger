using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Bina28.Models;

namespace ShiftsLogger.Bina28.Data;

public class ShiftsDbContext: DbContext
{
	public ShiftsDbContext(DbContextOptions options) : base(options)
	{
	}

	public DbSet<Shift> Shifts { get; set; }
}
