using Microsoft.EntityFrameworkCore;
using shiftsLogger.doc415.Models;

namespace shiftsLogger.doc415.Context;

public class ShiftDataContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Data source to be replaced with config connection string.
        optionsBuilder.UseSqlServer($"Server=(localdb)\\MSSQLLocalDB;Database=ShiftsLogger; Integrated Security=true;");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shift>()
            .HasKey(shift => new { shift.Id });
    }
    public DbSet<Shift> Shifts { get; set; }
}
