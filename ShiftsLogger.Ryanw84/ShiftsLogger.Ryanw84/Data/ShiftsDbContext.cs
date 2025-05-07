using System;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Ryanw84.Models;

namespace ShiftsLogger.Ryanw84.Models;

public class ShiftsDbContext : DbContext
{
    public DbSet<Shift> Shift { get; set; }
    public DbSet<Location> Location { get; set; }
    public DbSet<Worker> Worker { get; set; }


    public ShiftsDbContext(DbContextOptions<ShiftsDbContext> options)
        : base(options) { }

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
        // Configure Shift-Worker relationship
        modelBuilder
            .Entity<Shift>()
            .HasOne(s=>s.Worker) // A Shift has one Worker
            .WithMany(static w => w.Shifts) // A Worker has many Shifts
            .HasForeignKey(s => s.WorkerId) // Foreign key in Shift
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
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

public class Shift
{
    public int Id { get; set; }
    public string ShiftName { get; set; }
    public DateTimeOffset Date { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public int WorkerId { get; set; }
    public int LocationId { get; set; }
    public virtual Worker Worker { get; set; } // Changed from ICollection<Worker> to Worker
    public virtual Location Location { get; set; } // Changed from ICollection<Location> to Location
}
