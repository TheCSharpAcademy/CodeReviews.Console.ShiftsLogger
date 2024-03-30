using Microsoft.EntityFrameworkCore;
using ShiftsLoggerLibrary.Models;

namespace ShiftsLoggerAPI;

public class ShiftsContext(IConfiguration configuration) : DbContext
{
    public DbSet<Shift> Shifts { get; set; }
    private readonly IConfiguration _configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? connectionString = _configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("The connection string is not provided.");
        }

        optionsBuilder.UseSqlServer(connectionString);
    }
}