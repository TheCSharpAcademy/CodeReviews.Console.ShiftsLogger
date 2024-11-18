using Microsoft.EntityFrameworkCore;

namespace ShiftLogger.Mefdev.ShiftLoggerAPI.Models;

public class WorkerShiftContext : DbContext
{
    public WorkerShiftContext(DbContextOptions<WorkerShiftContext> options)
        : base(options)
    {
        
    }

    public DbSet<WorkerShift> WorkerShifts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkerShift>()
            .HasKey(e => e.Id);
    }
}