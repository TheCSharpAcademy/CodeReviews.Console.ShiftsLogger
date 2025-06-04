using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.DAL.Entities;

namespace ShiftsLogger.DAL;
public class ShiftsContext : DbContext
{
	public ShiftsContext(DbContextOptions<ShiftsContext> options) : base(options)
	{

	}
	public DbSet<UserEntity> Users { get; set; }
	public DbSet<ShiftEntity> Shifts { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}
