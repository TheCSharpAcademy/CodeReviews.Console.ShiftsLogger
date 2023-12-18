using Microsoft.EntityFrameworkCore;

namespace ShiftsLoggerAPI.Models;

public class ShiftsContext : DbContext
{
    public ShiftsContext(DbContextOptions<ShiftsContext> options)
        : base(options)
    {
    }
    public DbSet<Shift> Shifts { get; set; }
}