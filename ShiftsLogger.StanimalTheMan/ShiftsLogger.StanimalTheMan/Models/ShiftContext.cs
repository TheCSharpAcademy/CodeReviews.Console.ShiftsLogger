using Microsoft.EntityFrameworkCore;

namespace ShiftsLoggerWebAPI.Models;

public class ShiftContext : DbContext
{
	public ShiftContext(DbContextOptions<ShiftContext> options) : base(options)
	{
	}

	public DbSet<Shift> Shifts { get; set; } = null!;
}
