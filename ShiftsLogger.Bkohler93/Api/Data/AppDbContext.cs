using Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

# pragma warning disable CS1591
public class AppDbContext : DbContext {
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<WorkerShift> WorkerShifts { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}
# pragma warning restore CS1591