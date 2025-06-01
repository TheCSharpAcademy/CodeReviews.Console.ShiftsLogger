using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class ShiftsDbContext : DbContext
{
    public ShiftsDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Shift> Shifts { get; set; }
}