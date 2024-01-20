using Microsoft.EntityFrameworkCore;

namespace ShiftsLoggerWebApi.Models;
public class ShiftsLoggerContext : DbContext
{
    public static readonly int EmployeeNameLenght = 150;
    public static string? ShiftsLoggerConnectionString {get; set;}
    public DbSet<Employee> Employees {get; set;}
    public DbSet<Shift> Shifts {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options
        .UseSqlServer(ShiftsLoggerConnectionString,
        sqlServerOptions => sqlServerOptions.CommandTimeout(5))
        .LogTo(Console.WriteLine);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasKey(p => p.EmployeeId);

        modelBuilder.Entity<Employee>()
            .Property(p => p.EmployeeId)
            .UseIdentityColumn();

        modelBuilder.Entity<Employee>()
            .Property(p => p.Name)
            .HasMaxLength(EmployeeNameLenght)
            .IsRequired(true)
            .IsUnicode(true);

        modelBuilder.Entity<Employee>()
            .OwnsMany(p => p.Shifts)
            .Property(p => p.ShiftId)
            .UseIdentityColumn();   
    }
}