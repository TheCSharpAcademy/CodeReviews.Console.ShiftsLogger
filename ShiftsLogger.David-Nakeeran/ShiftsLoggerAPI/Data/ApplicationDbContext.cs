using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Employee> Employees { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
    {

    }
}