
using Microsoft.EntityFrameworkCore;

public class ShiftsLoggerDbContext : DbContext
{
    public ShiftsLoggerDbContext(DbContextOptions options) : base(options)
    {

    }
    
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Shift> Shifts { get; set; }
}