using Microsoft.EntityFrameworkCore;
using ShiftLoggerApi.Models;

namespace ShiftLoggerApi;

public class ShiftLoggerContext : DbContext
{
    public ShiftLoggerContext(DbContextOptions<ShiftLoggerContext> options) : base(options)
    {
    }

    public DbSet<Worker> Workers { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Department> Departments { get; set; }
}
