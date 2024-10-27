using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Models;
using System.Reflection.Metadata;

namespace ShiftsLoggerAPI
{

    public class ShiftsLoggerContext : DbContext
    {
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        public const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=ShiftsLoggerDB;Trusted_Connection=True";
        public ShiftsLoggerContext(DbContextOptions<ShiftsLoggerContext> options)
            : base(options)
        { }

        public ShiftsLoggerContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure zero-to-many relationship and cascade delete.
            modelBuilder.Entity<Worker>()
                .HasMany(w => w.Shifts)
                .WithOne(s => s.Worker)
                .HasForeignKey(s => s.WorkerId)
                .OnDelete(DeleteBehavior.Cascade);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

    }
}
