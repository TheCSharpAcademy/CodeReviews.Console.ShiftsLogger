using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Data;

public class ShitftsLoggerDbContext : DbContext
{
    public ShitftsLoggerDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Shift>? Shifts { get; set; }
    public DbSet<Employee>? Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasKey(emp => emp.EmpId);

        modelBuilder.Entity<Employee>()
            .HasIndex(emp => emp.EmpName)
            .IsUnique();

        modelBuilder.Entity<Shift>()
            .HasOne(shift => shift.Employee)
            .WithMany(emp => emp.EmpShifts)
            .HasForeignKey(shift => shift.EmpId);
    }
}