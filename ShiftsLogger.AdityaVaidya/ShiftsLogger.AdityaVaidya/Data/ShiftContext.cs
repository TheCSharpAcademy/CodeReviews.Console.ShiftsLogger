using Microsoft.EntityFrameworkCore;
using ShiftsLogger.AdityaVaidya.Models;

namespace ShiftsLogger.AdityaVaidya.Data;

public class ShiftContext: DbContext
{
    public DbSet<Shift> Shifts { get; set; } = null!;
    public DbSet<Worker> Workers { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) // Only configure if not already configured via DI
        {
            DotNetEnv.Env.Load();
            var connectionString = DotNetEnv.Env.GetString("CONNECTION_STRING");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shift>()
            .HasOne(s => s.Worker)
            .WithMany() // Assuming a worker can have many shifts
            .HasForeignKey(s => s.WorkerId)
            .OnDelete(DeleteBehavior.Cascade); // This enables cascade delete

        base.OnModelCreating(modelBuilder);
    }
}
