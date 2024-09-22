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
}
