using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Data;

public class ShiftsLoggerContext : DbContext
{
    public ShiftsLoggerContext(DbContextOptions<ShiftsLoggerContext> options) : base(options)
    {
    }

    public DbSet<Shift> Shifts { get; set; }
}
