using Microsoft.EntityFrameworkCore;
using ShiftLogger.Api.Models;

namespace ShiftLogger.Api.Data;

public class ApplicationDbContext : DbContext
{
    public  DbSet<Shift> Shifts{ get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Shift>().HasData(
            new Shift() 
        {
            Id = 1,
            EmployeeName = "Adam",
            Start = new DateTime(2024, 09, 26).AddHours(12).AddMinutes(30),
            End = new DateTime(2024, 09, 26).AddHours(15)
        },

        new Shift()
        {
            Id = 2,
            EmployeeName = "Eve",
            Start = new DateTime(2024, 09, 26).Add(new TimeSpan(0, 12, 00, 00)),
            End = new DateTime(2024, 09, 26).AddHours(16)
        }
        ); 

        
    }
}
