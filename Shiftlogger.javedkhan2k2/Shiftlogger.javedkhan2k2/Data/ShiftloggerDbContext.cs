using Microsoft.EntityFrameworkCore;
using Shiftlogger.Entities;

namespace Shiftlogger.Data;

public class ShiftloggerDbContext : DbContext
{
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Shift> Shifts { get; set; }

    public ShiftloggerDbContext(DbContextOptions<ShiftloggerDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            Console.Clear();
            Console.WriteLine("Please configure the DefaultConnection\n");
            System.Environment.Exit(0);
        }
    }

}