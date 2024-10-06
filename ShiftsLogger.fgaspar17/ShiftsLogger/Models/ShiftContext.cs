using Microsoft.EntityFrameworkCore;

namespace ShiftsLogger;

public class ShiftContext : DbContext
{
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Employee> Employees { get; set; }

    public ShiftContext(DbContextOptions<ShiftContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\shiftslog;Database=ShiftDB;Integrated Security=True;");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Employee>()
            .HasIndex(e => e.Name)
            .IsUnique();

        builder.Entity<Shift>()
            .HasOne(s => s.Employee)
            .WithMany(e => e.Shifts)
            .HasForeignKey(s => s.EmployeeId)
            .IsRequired();

        builder.Entity<Employee>()
        .HasData(
            new Employee { EmployeeId = 1, Name = "John" },
            new Employee { EmployeeId = 2, Name = "Albert" }
        );

        DateTime seedStartDate = new(2024, 9, 20, 8, 0, 0);

        builder.Entity<Shift>()
            .HasData(
                new Shift { ShiftId = 1, Start = seedStartDate, End = seedStartDate.AddMinutes(120), EmployeeId = 1 },
                new Shift { ShiftId = 2, Start = seedStartDate.AddMinutes(60), End = seedStartDate.AddMinutes(180), EmployeeId = 2 }
            );
    }
}