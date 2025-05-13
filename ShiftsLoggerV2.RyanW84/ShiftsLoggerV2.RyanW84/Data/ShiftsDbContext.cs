using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerV2.RyanW84.Models;

namespace ShiftsLoggerV2.RyanW84.Data;

public class ShiftsDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Worker> Workers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		modelBuilder
			.Entity<Shift>()
			.HasOne(s => s.Worker) // A Shifts has one Worker
			.WithMany(static w => w.Shifts) // A Worker has many Shifts
			.HasForeignKey(s => s.WorkerId) // Foreign key in Shifts
			.OnDelete(DeleteBehavior.Cascade);

		modelBuilder
		.Entity<Shift>()
		.HasOne(s => s.Location) // A LocationId has many Shifts
		.WithMany(l => l.Shifts) // A Location has many shifts
		.HasForeignKey(s => s.LocationId) // Foreign key in Shifts
		.OnDelete(DeleteBehavior.Cascade);
	}

    public void SeedData()
    {
        Shifts.RemoveRange(Shifts);
        Locations.RemoveRange(Locations);
		Workers.RemoveRange(Workers);

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

       

        var workers = new List<Worker>()
        {
            new Worker()
            {
                Name = "John Doe",
                Phone = "123-456-7890",
                Email = "John@Doe.com",
            },
            new Worker()
            {
                Name = "Jane Doe",
                Phone = "123-456-7892",
                Email = "Jane@Doe.com",
            },
            new Worker()
            {
                Name = "Jim Doe",
				Phone = "123-456-7893",
                Email = "Jim@yahoo.com",
			}
        };
		Workers.AddRange(workers);

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
