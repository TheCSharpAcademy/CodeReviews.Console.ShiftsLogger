using Microsoft.EntityFrameworkCore;
using ShiftsLogger.frockett.API.Models;
using ShiftsLogger.frockett.API.DTOs;

namespace ShiftsLogger.frockett.API.Data;

public class ShiftLogContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Shift> Shifts { get; set; }

    public ShiftLogContext(DbContextOptions<ShiftLogContext> options)
    : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Set the relationship between employee and shifts, and set up delete cascade
        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Shifts)
            .WithOne(e => e.Employee)
            .HasForeignKey(s => s.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlServer(@"Server=Crockett;Database=ShiftsLoggerDB;Trusted_Connection=True;TrustServerCertificate=True");

public DbSet<ShiftsLogger.frockett.API.DTOs.ShiftDto> ShiftDto { get; set; } = default!;

}
