using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Data;

public class ShiftsLoggerDbContext(DbContextOptions<ShiftsLoggerDbContext> options) : DbContext(options)
{
    public DbSet<Shift> Shifts => Set<Shift>();
    public DbSet<Employee> Employees => Set<Employee>();
}
