using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShiftLogger.Brozda.API.Models;
using System.Text.Json;

namespace ShiftLogger.Brozda.API.Data
{
    /// <summary>
    /// Represents the Entity Framework database context for the ShiftLogger application.
    /// </summary>
    public class ShiftLoggerContext : DbContext
    {
        public DbSet<AssignedShift> AssignedShifts { get; set; } = null!;
        public DbSet<Worker> Workers { get; set; } = null!;
        public DbSet<ShiftType> ShiftTypes { get; set; } = null!;

        /// <summary>
        /// Configures the database context with connection details, logging, and seeding behavior.
        /// </summary>
        /// <param name="optionsBuilder">A builder used to configure options for this context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<ShiftLoggerContext>()
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection")!;

            optionsBuilder.UseSqlServer(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .UseSeeding((dbContext, _) =>
                {
                    var path = Path.Combine(AppContext.BaseDirectory, "Resources", "SeedData.json");
                    if (File.Exists(path))
                    {
                        var rawData = File.ReadAllText(path);
                        var seedData = JsonSerializer.Deserialize<SeedData>(rawData);

                        if (seedData is not null)
                        {
                            dbContext.Set<ShiftType>().AddRange(seedData.ShiftTypes);
                            dbContext.Set<Worker>().AddRange(seedData.Workers);

                            dbContext.SaveChanges();

                            dbContext.Set<AssignedShift>().AddRange(seedData.AssignedShifts);

                            dbContext.SaveChanges();
                        }
                    }
                });
        }
    }
}