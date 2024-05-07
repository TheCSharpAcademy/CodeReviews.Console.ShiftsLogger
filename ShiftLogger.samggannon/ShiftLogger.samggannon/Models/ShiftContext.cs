using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace ShiftLogger.samggannon.Models;

public class ShiftContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ShiftContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<WorkShift> Shifts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DataConnection"));
    }
}
