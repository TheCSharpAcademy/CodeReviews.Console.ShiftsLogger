using Microsoft.EntityFrameworkCore;
using ShiftsLoggerWebAPI.Models;

namespace ShiftsLoggerWebAPI.Data;

public class ShiftsDbContext : DbContext
{
    public string _connectionString;
    public ShiftsDbContext(DbContextOptions<ShiftsDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Shift> Shifts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().ToTable("Employee");
        modelBuilder.Entity<Shift>().ToTable("Shift");
        modelBuilder.Entity<Shift>()
            .HasOne(e => e.Employee)
            .WithMany(e => e.Shifts)
            .HasForeignKey(s => s.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}