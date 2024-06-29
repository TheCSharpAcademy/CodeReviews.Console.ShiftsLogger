using Microsoft.EntityFrameworkCore;
using ShiftsLoggerApi.Database;
using ShiftsLoggerApi.Shifts.Models;
using ShiftsLoggerApi.Employees.Models;

namespace ShiftsLoggerApi;

public class ShiftsLoggerContext : DbContext
{
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ConnectionManager.Init();

        optionsBuilder.UseSqlServer(
            ConnectionManager.GetConnectionString(
                ConfigManager.Database["Name"]
            )
        );
    }
}

