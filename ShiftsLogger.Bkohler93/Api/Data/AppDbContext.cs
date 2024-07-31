using Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext {
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<WorkerShift> WorkerShifts { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}