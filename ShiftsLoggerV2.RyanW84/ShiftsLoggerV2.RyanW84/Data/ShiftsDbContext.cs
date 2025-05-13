using Microsoft.EntityFrameworkCore;
using ShiftsLoggerV2.RyanW84.Models;

namespace ShiftsLoggerV2.RyanW84.Data;

public class ShiftsDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Location> Locations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Shift>()
            .HasOne(s => s.Location)
            .WithOne(l => l.Shift)
            .HasForeignKey<Location>(L => L.LocationId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public void SeedData()
    {
        Shifts.RemoveRange(Shifts);
        Locations.RemoveRange(Locations);

        var locations = new List<Location>()
        {
            new Location
            {
                Name = "Colchester General Hospital",
                Address = "Turner Road",
                TownOrCity = "Colchester",
                StateOrCounty = "Essex",
                ZipOrPostCode = "CO4 5JL",
                Country = "England",
            },
            new Location
            {
                Name = "The Royal Brisbane and Women's Hospital",
                Address = "Butterfield Street",
                TownOrCity = "Herston",
                StateOrCounty = "Queensland",
                ZipOrPostCode = "QLD 4006",
                Country = "Australia",
            },
            new Location
            {
                Name = "Location 3",
                Address = "601 E Rollins Street",
                TownOrCity = "Orlando",
                StateOrCounty = "Florida",
                ZipOrPostCode = "FL 32803",
                Country = "USA",
            },
        };

        Locations.AddRange(locations);

        // Seed the database with some initial data
        Shifts.AddRange(
            new Shift
            {
                WorkerId = 1,
                StartTime = DateTimeOffset.UtcNow.AddHours(2),
                EndTime = DateTimeOffset.UtcNow.AddHours(10),
                Location = locations[0],
            },
            new Shift
            {
                WorkerId = 2,
                StartTime = DateTimeOffset.UtcNow.AddHours(1),
                EndTime = DateTimeOffset.UtcNow.AddHours(5),
                Location = locations[1],
            },
            new Shift
            {
                WorkerId = 3,
                StartTime = DateTimeOffset.UtcNow.AddHours(3),
                EndTime = DateTimeOffset.UtcNow.AddHours(8),
                Location = locations[2],
            }
        );
        SaveChanges();
    }
}
