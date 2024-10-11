using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShiftsLogger.API.Data.Configurations;

namespace ShiftsLogger.API.Data
{
  public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
  {
    public DbSet<Shift> Shifts { get; set; }

    public DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new WorkerConfiguration());
      base.OnModelCreating(modelBuilder);
    }
  }
}
