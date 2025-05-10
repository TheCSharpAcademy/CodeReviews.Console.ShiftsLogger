using Microsoft.EntityFrameworkCore;
using ShiftsLogger.SpyrosZoupas.DAL.Model;

namespace ShiftsLogger.SpyrosZoupas.DAL;

public class ShiftsLoggerDbContext : DbContext
{
    public ShiftsLoggerDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Shift> Shifts { get; set; }
}
