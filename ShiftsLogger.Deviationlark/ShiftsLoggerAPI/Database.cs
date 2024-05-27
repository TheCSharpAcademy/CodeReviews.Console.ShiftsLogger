using Microsoft.EntityFrameworkCore;

namespace ShiftsLogger;

public class ShiftsContext : DbContext
{
    public DbSet<ShiftModel> Shifts { get; set; }

    public ShiftsContext(DbContextOptions<ShiftsContext> options) : base(options)
    {
    }
}