using Microsoft.EntityFrameworkCore;

namespace ShiftApi.Models;

public class ShiftApiContext : DbContext
{
    public ShiftApiContext(DbContextOptions<ShiftApiContext> options) : base(options)
    {
    }

    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Employee> Employees { get; set; }
}
