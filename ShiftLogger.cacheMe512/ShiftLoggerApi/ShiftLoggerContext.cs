using Microsoft.EntityFrameworkCore;
using ShiftLoggerApi.Models;

namespace ShiftLoggerApi;

public class ShiftLoggerContext : DbContext
{
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Department> Departments { get; set; }

    public ShiftLoggerContext(DbContextOptions<ShiftLoggerContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shift>()
            .HasOne(s => s.Worker)
            .WithMany(w => w.Shifts)
            .HasForeignKey(s => s.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Worker>()
            .HasOne(w => w.Department)
            .WithMany(d => d.Workers)
            .HasForeignKey(w => w.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
