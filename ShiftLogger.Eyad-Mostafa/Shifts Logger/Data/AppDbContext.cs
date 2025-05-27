using Microsoft.EntityFrameworkCore;
using Shifts_Logger.Models;

namespace Shifts_Logger.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Shift> Shifts { get; set; } = null!;
    public DbSet<Worker> Workers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Worker>()
            .HasMany(w => w.Shifts)
            .WithOne(s => s.Worker)
            .HasForeignKey(s => s.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
