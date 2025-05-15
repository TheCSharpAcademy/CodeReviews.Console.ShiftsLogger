using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShiftsLoggerAPI.Models;

namespace ShiftsLogger.KamilKolanowski.Models.Data;

internal class ShiftsLoggerDbContext : DbContext
{
    public ShiftsLoggerDbContext(DbContextOptions<ShiftsLoggerDbContext> options)
        : base(options) { }

    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Worker> Workers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("TCSA");
        base.OnModelCreating(modelBuilder);
    }
}
