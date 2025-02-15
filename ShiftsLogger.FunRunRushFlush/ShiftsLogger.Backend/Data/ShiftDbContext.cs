using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Domain;

namespace ShiftsLogger.Backend.Data;

public class ShiftDbContext : DbContext
{
    public ShiftDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }


    public DbSet<Shift> ShiftsTable { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfiguration(new ShiftConfiguration());
    }
}
