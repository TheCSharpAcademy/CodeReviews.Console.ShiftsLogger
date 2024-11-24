using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShiftsLoggerApi.Model;

namespace ShiftsLoggerApi.Controllers;

public class AppDbContext : DbContext
{
    public DbSet<Shift> Shifts { get; set; }


    private readonly IConfiguration _configuration;
    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString).UseLazyLoadingProxies();
        optionsBuilder.UseSqlServer(connectionString).LogTo(Console.WriteLine, LogLevel.None);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shift>();
    }
}