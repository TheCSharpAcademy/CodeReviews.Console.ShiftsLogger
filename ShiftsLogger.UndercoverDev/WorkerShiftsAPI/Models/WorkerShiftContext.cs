using Microsoft.EntityFrameworkCore;

namespace WorkerShiftsAPI.Models;
public class WorkerShiftContext : DbContext
{
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Shift> Shifts { get; set; }

    public WorkerShiftContext(DbContextOptions<WorkerShiftContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Worker>()
            .HasMany(w => w.Shifts)
            .WithOne(s => s.Worker)
            .HasForeignKey(s => s.WorkerId);

        modelBuilder.Entity<Shift>()
            .HasOne(s => s.Worker)
            .WithMany(w => w.Shifts)
            .HasForeignKey(s => s.WorkerId);
    }
}