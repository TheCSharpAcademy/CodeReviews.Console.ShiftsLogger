using Microsoft.EntityFrameworkCore;
using ShiftsLogger.yemiodetola.Models;

namespace ShfitsLogger.yemiodetola.Contexts;

public class ShiftsContext : DbContext
{
  public ShiftsContext(DbContextOptions options) : base(options)
  {

  }

  public DbSet<Shift> Shifts { get; set; }
}
