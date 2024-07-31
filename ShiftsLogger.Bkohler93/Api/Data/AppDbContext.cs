using Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

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

        modelBuilder.Entity<WorkerShift>()
            .HasOne(ws => ws.Worker)                 // A WorkerShift has one Worker
            .WithMany(w => w.WorkerShifts)           // A Worker has many WorkerShifts
            .HasForeignKey(ws => ws.WorkerId);       // The foreign key in WorkerShift is WorkerId

        modelBuilder.Entity<WorkerShift>()
            .HasOne(ws => ws.Shift)                  // A WorkerShift has one Shift
            .WithMany(s => s.WorkerShifts)           // A Shift has many WorkerShifts
            .HasForeignKey(ws => ws.ShiftId);        // The foreign key in WorkerShift is ShiftId
    }
}