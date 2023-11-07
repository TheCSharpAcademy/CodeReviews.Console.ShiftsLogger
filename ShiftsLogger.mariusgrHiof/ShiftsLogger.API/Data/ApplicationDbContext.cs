using Microsoft.EntityFrameworkCore;
using ShiftsLogger.API.Models;

namespace ShiftsLogger.API.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Shift> Shifts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Worker>().HasData(
            new Worker
            {
                Id = 1,
                FirstName = "Marius",
                LastName = "Gravningsmyhr",
            },
            new Worker
            {
                Id = 2,
                FirstName = "Ola",
                LastName = "Nordmann",
            }
            );
    }
}