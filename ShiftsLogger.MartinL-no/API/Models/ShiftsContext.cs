using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class ShiftsContext : DbContext
{
    public DbSet<Shift> Shifts { get; set; } = null!;

    public ShiftsContext(DbContextOptions<ShiftsContext> options)
           : base(options)
    {
    }
}
