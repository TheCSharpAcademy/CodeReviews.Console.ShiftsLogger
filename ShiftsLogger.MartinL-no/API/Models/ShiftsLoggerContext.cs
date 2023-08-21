using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class ShiftsLoggerContext : DbContext
{
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Shift> Shifts { get; set; } = null!;

    public ShiftsLoggerContext(DbContextOptions<ShiftsLoggerContext> options)
        : base(options)
    {
    }
}
