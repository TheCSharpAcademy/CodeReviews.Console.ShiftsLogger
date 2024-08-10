using Microsoft.EntityFrameworkCore;
using Shared;

namespace Server.Data;

public class ShiftLoggerContext : DbContext
{
  public ShiftLoggerContext(DbContextOptions<ShiftLoggerContext> options)
      : base(options)
  {
  }
  public DbSet<Employee> Employees { get; set; } = null!;
  public DbSet<Shift> Shifts { get; set; } = null!;
  public DbSet<EmployeeShift> EmployeeShifts { get; set; } = null!;


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<EmployeeShift>()
      .HasKey(es => new { es.EmployeeId, es.ShiftId });

    modelBuilder.Entity<EmployeeShift>()
      .HasOne(es => es.Employee)
      .WithMany(e => e.EmployeeShifts)
      .HasForeignKey(es => es.EmployeeId);

    modelBuilder.Entity<EmployeeShift>()
      .HasOne(es => es.Shift)
      .WithMany(s => s.EmployeeShifts)
      .HasForeignKey(es => es.ShiftId);

    modelBuilder.Entity<EmployeeShift>()
      .HasIndex(es => es.ClockInTime);
  }
}