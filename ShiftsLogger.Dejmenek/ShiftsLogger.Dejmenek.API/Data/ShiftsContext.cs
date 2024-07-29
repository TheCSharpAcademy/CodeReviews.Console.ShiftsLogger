using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Dejmenek.API.Models;

namespace ShiftsLogger.Dejmenek.API.Data;

public class ShiftsContext : DbContext
{
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Employee> Employees { get; set; }

    public ShiftsContext(DbContextOptions<ShiftsContext> options) : base(options)
    {

    }
}
