using Microsoft.EntityFrameworkCore;

namespace ShiftsLogger.API.Models.Contexts;

public class ShiftContext : DbContext
{
    public ShiftContext(DbContextOptions<ShiftContext> options) : base(options)
    {
        
    }

    public DbSet<Shift> Shifts { get; set; } = null!;
}