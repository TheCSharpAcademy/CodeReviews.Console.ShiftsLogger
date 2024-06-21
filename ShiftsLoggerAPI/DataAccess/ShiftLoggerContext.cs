using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;

namespace ShiftsLoggerAPI.DataAccess
{
    public class ShiftLoggerContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ShiftLoggerContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shift>()
                .HasOne(e => e.Employee)
                .WithMany(s => s.Shifts)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Name = "John Doe", DateOfBirth = new DateTime(1980, 1, 1), PhoneNumber = "123-456-7890", EmailAddress = "johndoe@example.com" },
                new Employee { Id = 2, Name = "Jane Doe", DateOfBirth = new DateTime(1985, 5, 15), PhoneNumber = "987-654-3210", EmailAddress = "janedoe@example.com" }
            );

            modelBuilder.Entity<Shift>().HasData(
                new Shift { Id = 1, EmployeeId = 1, StartTime = new DateTime(2023, 3, 1, 8, 0, 0), EndTime = new DateTime(2023, 3, 1, 16, 0, 0) },
                new Shift { Id = 2, EmployeeId = 1, StartTime = new DateTime(2023, 3, 2, 8, 0, 0), EndTime = new DateTime(2023, 3, 2, 16, 0, 0) },
                new Shift { Id = 3, EmployeeId = 2, StartTime = new DateTime(2023, 3, 1, 12, 0, 0), EndTime = new DateTime(2023, 3, 1, 20, 0, 0) }
            );
        }
    }
}
