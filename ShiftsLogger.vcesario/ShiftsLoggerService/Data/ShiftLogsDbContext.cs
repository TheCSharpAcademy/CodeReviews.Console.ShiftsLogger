using Microsoft.EntityFrameworkCore;

public class ShiftLogsDbContext : DbContext
{
    public ShiftLogsDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Shift> Shifts { get; set; }
}