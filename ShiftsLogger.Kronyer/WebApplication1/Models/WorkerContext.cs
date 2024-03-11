using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class WorkerContext : DbContext
    {
        public DbSet<Worker> Worker { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(LocalDb)\LocalDb;Database=ShiftLogger;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Worker>().HasIndex(x => x.Name).IsUnique();
        }
    }
}
