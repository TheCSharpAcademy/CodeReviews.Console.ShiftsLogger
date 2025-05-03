using System;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Ryanw84.Models;

namespace ShiftsLogger.Ryanw84.Data;

public class ShiftsDbContext : DbContext
{
    public ShiftsDbContext(DbContextOptions<ShiftsDbContext> options)
        : base(options) { }

    public DbSet<Shift> Shift { get; set; }
    public DbSet<Location> Location { get; set; }
    public DbSet<Worker> Worker { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder
            .UseSqlServer(
                @"Server=(localdb)\MSSQLlocaldb; Database = ShiftsLogger; initial Catalog=ShiftsLogger; Integrated Security=True; TrustServerCertificate=True;"
            )
            .EnableSensitiveDataLogging()
            .UseLoggerFactory(GetLoggerFactory());
    }

    private static ILoggerFactory GetLoggerFactory( )
        {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.AddFilter(
                (category , level) =>
                    category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information
            );
        });
        return loggerFactory;
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<Shift>()
            .HasOne(s => s.Worker)
            .WithMany(w => w.Shifts)
            .HasForeignKey(s => s.WorkerId);

        modelBuilder.Entity<Shift>()
            .HasOne(s => s.Location)
            .WithMany(l => l.Shifts)
            .HasForeignKey(s => s.LocationId);
        }
}
