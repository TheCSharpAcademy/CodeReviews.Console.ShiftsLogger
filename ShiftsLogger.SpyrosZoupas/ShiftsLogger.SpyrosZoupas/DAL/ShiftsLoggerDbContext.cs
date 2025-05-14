using Microsoft.EntityFrameworkCore;
using ShiftsLogger.SpyrosZoupas.DAL.Model;

namespace ShiftsLogger.SpyrosZoupas.DAL;

public class ShiftsLoggerDbContext : DbContext
{
    public ShiftsLoggerDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Shift> Shifts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shift>()
            .HasData(new List<Shift>
            {
                    new Shift
                    {
                        ShiftId = 1,
                        StartDateTime = new DateTime(2025, 01, 01),
                        EndDateTime = new DateTime(2025, 02, 01),
                    },
                    new Shift
                    {
                        ShiftId = 2,
                        StartDateTime = new DateTime(2025, 01, 01),
                        EndDateTime = new DateTime(2025, 01, 05)
                    },
                    new Shift
                    {
                        ShiftId = 3,
                        StartDateTime = new DateTime(2025, 01, 01),
                        EndDateTime = new DateTime(2026, 01, 01)
                    },
                    new Shift
                    {
                        ShiftId = 4,
                        StartDateTime = new DateTime(2025, 11, 10), 
                        EndDateTime = new DateTime(2025, 12, 20)
                    }
            });
    }

}
