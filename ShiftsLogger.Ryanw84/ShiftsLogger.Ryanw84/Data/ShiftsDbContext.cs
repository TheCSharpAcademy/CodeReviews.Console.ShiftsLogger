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

    private static ILoggerFactory GetLoggerFactory()
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.AddFilter(
                (category, level) =>
                    category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information
            );
        });
        return loggerFactory;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Shift>()
            .HasOne(s => s.Worker)
            .WithMany(w => w.ShiftsWorked)
            .HasForeignKey(s => s.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Shift>()
            .HasOne(s => s.Location)
            .WithMany()
            .HasForeignKey(s => s.LocationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Worker>().Property(w => w.Name).IsRequired().HasMaxLength(100);
    }

    public void SeedData()
    {
        Shift.RemoveRange(Shift);
        Location.RemoveRange(Location);
        Worker.RemoveRange(Worker);

        var worker1 = new Worker { Name = "John Doe" };
        var worker2 = new Worker { Name = "Jane Smith" };
        var worker3 = new Worker { Name = "Alice Johnson" };
        var worker4 = new Worker { Name = "Bob Brown" };

        var location1 = new Location { Name = "Colchester General Hospital" };
        var location2 = new Location { Name = "Royal Adelaide Hospital" };
        var location3 = new Location { Name = "New York Presbyterian Hospital" };
        var location4 = new Location { Name = "Toronto Western Hospital" };

        Shift.AddRange(
            new Shift
            {
                Date = DateTimeOffset.Now,
                StartTime = DateTimeOffset.Now.AddHours(5),
                EndTime = DateTimeOffset.Now.AddHours(11),
                Worker = worker1,
                Location = location1,
            },
            new Shift
            {
                Date = DateTimeOffset.Now,
                StartTime = DateTimeOffset.Now.AddHours(8),
                EndTime = DateTimeOffset.Now.AddHours(13),
                Worker = worker2,
                Location = location2,
            },
            new Shift
            {
                Date = DateTimeOffset.Now,
                StartTime = DateTimeOffset.Now.AddHours(4),
                EndTime = DateTimeOffset.Now.AddHours(9),
                Worker = worker3,
                Location = location3,
            },
            new Shift
            {
                Date = DateTimeOffset.Now,
                StartTime = DateTimeOffset.Now.AddHours(12),
                EndTime = DateTimeOffset.Now.AddHours(4),
                Worker = worker4,
                Location = location4,
            }
        );
        Location.AddRange(location1, location2, location3, location4);
        Worker.AddRange(worker1, worker2, worker3, worker4);
        SaveChanges();
    }
}
