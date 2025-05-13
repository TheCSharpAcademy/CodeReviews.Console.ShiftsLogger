using Microsoft.EntityFrameworkCore;
using ShiftsLogger.weakiepedia.Models;

namespace ShiftsLogger.weakiepedia.Data;

public class ShiftsDbContext : DbContext
{
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Employee> Employees { get; set; }
    
    public ShiftsDbContext(DbContextOptions<ShiftsDbContext> options) : base(options)
    {
        
    }
}