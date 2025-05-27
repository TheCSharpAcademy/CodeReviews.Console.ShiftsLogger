using Microsoft.EntityFrameworkCore;
using ShfitsLogger.yemiodetola.Models;

namespace ShfitsLogger.yemiodetola.Contexts;

public class ShiftsContext : DbContext
{
  public ShiftsContext(DbContextOptions options) : base(options)
  {

  }

  public DbSet<Shift> Shifts { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<Shift>(entity =>
        {
          entity.HasKey(e => e.Id);
          entity.Property(e => e.Name).HasMaxLength(100);
        });
  }
}
