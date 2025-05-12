using Microsoft.EntityFrameworkCore;
using ShiftWebApi.Models;

namespace ShiftWebApi.Data;
public class ShiftDbContext : DbContext
{
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<User> Users { get; set; }

    // change to false if you don't want the db to always be deleted/recreated for test purpose
    private static bool resetDBForTest { get; set; } = true;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shift>().HasKey(s => new { s.Id });
        modelBuilder.Entity<Shift>().HasOne(s => s.User).WithMany(u => u.Shifts).HasForeignKey(u => u.UserId);
        modelBuilder.Entity<User>().HasKey(u => new { u.UserId });

        List<User> users = new(){ new User { UserId = 1, FirstName = "Luffy", LastName = "Monkey D.", IsActive = true },
            new User { UserId = 2, FirstName = "Roger", LastName = "Gold", IsActive = false },
            new User { UserId = 3, FirstName = "Zoro", LastName = "Roronoa", IsActive = true }
        };

        modelBuilder.Entity<User>().HasData(users);

        modelBuilder.Entity<Shift>().HasData(
            new Shift { Id = 1, StartTime = DateTime.Now.AddDays(-10), EndTime = DateTime.Now.AddDays(-10).AddHours(8), UserId = 1 },
            new Shift { Id = 2, StartTime = DateTime.Now.AddDays(-20), EndTime = DateTime.Now.AddDays(-20).AddHours(8), UserId = 2},
            new Shift { Id = 3, StartTime = DateTime.Now.AddDays(-10), EndTime = DateTime.Now.AddDays(-10).AddHours(8), UserId = 3},
            new Shift { Id = 4, StartTime = DateTime.Now.AddDays(-9), EndTime = DateTime.Now.AddDays(-9).AddHours(8), UserId = 1},
            new Shift { Id = 5, StartTime = DateTime.Now.AddDays(-19), EndTime = DateTime.Now.AddDays(-19).AddHours(8), UserId = 2},
            new Shift { Id = 6, StartTime = DateTime.Now.AddDays(-9), EndTime = DateTime.Now.AddDays(-9).AddHours(8), UserId = 3},
            new Shift { Id = 7, StartTime = DateTime.Now.AddDays(-5), EndTime = DateTime.Now.AddDays(-5).AddHours(8), UserId = 1},
            new Shift { Id = 8, StartTime = DateTime.Now.AddDays(-15), EndTime = DateTime.Now.AddDays(-15).AddHours(8), UserId = 2},
            new Shift { Id = 9, StartTime = DateTime.Now.AddDays(-4), EndTime = DateTime.Now.AddDays(-4).AddHours(8), UserId = 3},
            new Shift { Id = 10, StartTime = DateTime.Now.AddDays(-3), EndTime = DateTime.Now.AddDays(-3).AddHours(8), UserId = 1 }
            );
    }
    public ShiftDbContext(DbContextOptions options) : base(options)
    {
        if (resetDBForTest)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            resetDBForTest = false;
        }
    }
}