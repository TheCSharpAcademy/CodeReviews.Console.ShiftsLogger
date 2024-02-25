using Buutyful.ShiftsLogger.Domain;
using Microsoft.EntityFrameworkCore;

namespace Buutyful.ShiftsLogger.Api.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Worker> Workers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed Workers
        modelBuilder.Entity<Worker>().HasData(
            Worker.CreateWithId(Guid.Parse("6CE80C50-DA19-4035-9E7F-3061F20A17E0"), "Worker1", Role.None),
            Worker.Create("Worker2", Role.Employee),
            Worker.Create("Worker3", Role.Manager));

        // Seed Shifts
        modelBuilder.Entity<Shift>().HasData(
          Shift.Create(
              Guid.Parse("6CE80C50-DA19-4035-9E7F-3061F20A17E0"),
              DateTime.Today,
              DateTime.Now,
              DateTime.Now.AddHours(8)));
    }

}
