using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI;

public class ShiftsContext : DbContext
{
  public ShiftsContext(DbContextOptions<ShiftsContext> options) : base(options) { }

  public DbSet<Shift> Shifts { get; set; }
}