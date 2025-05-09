using Microsoft.EntityFrameworkCore;

namespace ShiftsLogger.Ryanw84.Data;

public class ShiftsDbContext: DbContext
	{
	public ShiftsDbContext(DbContextOptions<ShiftsDbContext> options) : base(options)
		{
		}

	// Define your DbSets here, for example:
	// public DbSet<Shift> Shifts { get; set; }
	}
	
		
		
