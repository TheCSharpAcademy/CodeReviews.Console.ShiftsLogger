using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.DataAccess
{
	public class ShiftsLoggerContext : DbContext
	{
		public ShiftsLoggerContext(DbContextOptions<ShiftsLoggerContext> options) : base(options) 
		{ 
		}
		public DbSet<ShiftModel> Shifts { get; set; }
	}
}
