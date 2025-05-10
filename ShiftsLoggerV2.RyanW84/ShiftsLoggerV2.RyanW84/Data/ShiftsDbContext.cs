using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerV2.RyanW84.Models;

namespace ShiftsLoggerV2.RyanW84.Data;

public class ShiftsDbContext : DbContext
{
    public ShiftsDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Shift> Shifts { get; set; }

    public void SeedData()
    {
        Shifts.RemoveRange(Shifts);
        SaveChanges();

        // Seed the database with some initial data
        Shifts.AddRange(
            new Shift
            {
                workerId = 1,
                StartTime = DateTimeOffset.UtcNow,
                EndTime = DateTimeOffset.UtcNow.AddHours(8),
                LocationId = 1,
            },
            new Shift
            {
                workerId = 2,
                StartTime = DateTimeOffset.UtcNow,
                EndTime = DateTimeOffset.UtcNow.AddHours(8),
                LocationId = 2,
            },
            new Shift
            {
                workerId = 3,
                StartTime = DateTimeOffset.UtcNow,
                EndTime = DateTimeOffset.UtcNow.AddHours(8),
                LocationId = 3,
            }
        );
        SaveChanges();
    }
}
