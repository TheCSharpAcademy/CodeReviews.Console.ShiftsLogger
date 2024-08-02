using Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

# pragma warning disable CS1591
public class AppDbContext : DbContext {
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<WorkerShift> WorkerShifts { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Workers
        modelBuilder.Entity<Worker>().HasData(
            new Worker { Id = 1, FirstName = "John", LastName = "Doe", Position = "Manager" },
            new Worker { Id = 2, FirstName = "Jane", LastName = "Smith", Position = "Developer" },
            new Worker { Id = 3, FirstName = "Emily", LastName = "Johnson", Position = "Designer" }
        );

        // Seed Shifts
        modelBuilder.Entity<Shift>().HasData(
            new Shift { Id = 1, Name = "Morning Shift", StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(13, 0) },
            new Shift { Id = 2, Name = "Afternoon Shift", StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(17, 0) },
            new Shift { Id = 3, Name = "Evening Shift", StartTime = new TimeOnly(17, 0), EndTime = new TimeOnly(21, 0) }
        );

        // Seed WorkerShifts
        modelBuilder.Entity<WorkerShift>().HasData(
            new WorkerShift { Id = 1, WorkerId = 1, ShiftId = 1, ShiftDate = new DateOnly(2024, 8, 1) },
            new WorkerShift { Id = 2, WorkerId = 1, ShiftId = 2, ShiftDate = new DateOnly(2024, 8, 2) },
            new WorkerShift { Id = 3, WorkerId = 2, ShiftId = 1, ShiftDate = new DateOnly(2024, 8, 1) },
            new WorkerShift { Id = 4, WorkerId = 2, ShiftId = 3, ShiftDate = new DateOnly(2024, 8, 2) },
            new WorkerShift { Id = 5, WorkerId = 3, ShiftId = 2, ShiftDate = new DateOnly(2024, 8, 1) },
            new WorkerShift { Id = 6, WorkerId = 3, ShiftId = 3, ShiftDate = new DateOnly(2024, 8, 2) }
        );
    }
}
# pragma warning restore CS1591