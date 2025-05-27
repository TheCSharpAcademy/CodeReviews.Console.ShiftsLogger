using Microsoft.EntityFrameworkCore;
using ShiftsLogger.API.Data.Configurations;

namespace ShiftsLogger.API.Data
{
  //needed for builder.Services.AddDbContext<ApplicationDbContext>
  public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
  {
    public DbSet<Shift> Shifts { get; set; }

    public DbSet<Worker> Workers { get; set; }

    //Used for the options. Important when created the connectioning the database in the Program.cs
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
