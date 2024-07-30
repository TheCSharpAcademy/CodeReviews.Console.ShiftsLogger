using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data;

public class ShiftLoggerContext : DbContext
{
  public DbSet<Employee> Employees { get; set; }
  public DbSet<Shift> Shifts { get; set; }
  public DbSet<EmployeeShift> EmployeeShifts { get; set; }

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
  }
}