using Microsoft.EntityFrameworkCore;
using ShiftLogger_WebAPI.Arashi256.Config;

namespace ShiftLogger_WebAPI.Arashi256.Models
{
    public class ShiftLoggerContext : DbContext
    {
        private DatabaseConnection _connection;

        public ShiftLoggerContext()
        {
            _connection = new DatabaseConnection();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connection.DatabaseConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure zero-to-many relationship and cascade delete.
            modelBuilder.Entity<Worker>()
                .HasMany(w => w.WorkerShifts)
                .WithOne(s => s.Worker)
                .HasForeignKey(s => s.WorkerId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<WorkerShift> WorkerShifts { get; set; }
        public DbSet<Worker> Workers { get; set; }
    }
}
