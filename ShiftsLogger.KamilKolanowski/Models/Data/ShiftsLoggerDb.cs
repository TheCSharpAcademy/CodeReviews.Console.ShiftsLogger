using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ShiftsLogger.KamilKolanowski.Models.Data;

internal class ShiftsLoggerDb
{
    internal class ShiftsLoggerDbContext : DbContext
    {
        internal DbSet<Shift> Shifts { get; set; }
        internal DbSet<ShiftType> ShiftTypes { get; set; }
        internal DbSet<Worker> Workers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TCSA");
            base.OnModelCreating(modelBuilder);
        }
    }
}
