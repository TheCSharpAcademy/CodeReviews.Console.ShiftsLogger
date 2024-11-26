using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShiftsLoggerApi.Model;

namespace ShiftsLoggerApi.Controllers;

public class AppDbContext : DbContext
{
    public DbSet<Shift> Shifts { get; set; }


    private readonly IConfiguration _configuration;
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shift>();
    }
}