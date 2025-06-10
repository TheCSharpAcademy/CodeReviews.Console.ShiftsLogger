using Microsoft.EntityFrameworkCore;
using ShiftsLoggerV2.RyanW84.Models;

namespace ShiftsLoggerV2.RyanW84.Data;

public class ShiftsDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Shifts> Shifts { get; set; }
    public DbSet<Locations> Locations { get; set; }
    public DbSet<Workers> Workers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Shifts>()
            .HasOne(s => s.Worker)
            .WithMany(w => w.Shifts)
            .HasForeignKey(s => s.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder
            .Entity<Shifts>()
            .HasOne(s => s.Location)
            .WithMany(l => l.Shifts)
            .HasForeignKey(s => s.LocationId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder
            .Entity<Locations>()
            .HasMany(l => l.Shifts)
            .WithOne(s => s.Location)
            .HasForeignKey(s => s.LocationId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public void SeedData()
    {
        Shifts.RemoveRange(Shifts); // ? Cascade delete ?
        Locations.RemoveRange(Locations);
        Workers.RemoveRange(Workers);

        var locations = new List<Locations>
        {
            new Locations
            {
                Name = "Colchester General Hospital",
                Address = "Turner Road",
                TownOrCity = "Colchester",
                StateOrCounty = "Essex",
                ZipOrPostCode = "CO4 5JL",
                Country = "England",
            },
            new Locations
            {
                Name = "The Royal Brisbane and Women's Hospital",
                Address = "Butterfield Street",
                TownOrCity = "Herston",
                StateOrCounty = "Queensland",
                ZipOrPostCode = "QLD 4006",
                Country = "Australia",
            },
            new Locations
            {
                Name = "Advent Health",
                Address = "601 E Rollins Street",
                TownOrCity = "Orlando",
                StateOrCounty = "Florida",
                ZipOrPostCode = "FL 32803",
                Country = "USA",
            },
        };

        Locations.AddRange(locations);

        var workers = new List<Workers>
        {
            new Workers
            {
                Name = "John Doe",
                PhoneNumber = "123-456-7890",
                Email = "John@Doe.com",
            },
            new Workers
            {
                Name = "Jane Doe",
                PhoneNumber = "123-456-7892",
                Email = "Jane@Doe.com",
            },
            new Workers
            {
                Name = "Jim Doe",
                PhoneNumber = "123-456-7893",
                Email = "Jim@yahoo.com",
            },
        };
        Workers.AddRange(workers);

        // Save changes to generate IDs
        SaveChanges();

        // Retrieve the generated WorkerIds
        var workerIds = Workers.Select(w => w.WorkerId).ToList();

        Shifts.AddRange(
            new Shifts
            {
                WorkerId = workerIds[0],
                StartTime = DateTimeOffset.UtcNow.AddHours(2),
                EndTime = DateTimeOffset.UtcNow.AddHours(10),
                Location = locations[0],
            },
            new Shifts
            {
                WorkerId = workerIds[1],
                StartTime = DateTimeOffset.UtcNow.AddHours(1),
                EndTime = DateTimeOffset.UtcNow.AddHours(5),
                Location = locations[1],
            },
            new Shifts
            {
                WorkerId = workerIds[2],
                StartTime = DateTimeOffset.UtcNow.AddHours(3),
                EndTime = DateTimeOffset.UtcNow.AddHours(8),
                Location = locations[2],
            }
        );

        SaveChanges();
    }
}
