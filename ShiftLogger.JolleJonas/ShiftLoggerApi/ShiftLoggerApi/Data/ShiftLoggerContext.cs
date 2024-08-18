using Microsoft.EntityFrameworkCore;
using ShiftLoggerApi.Models;


namespace ShiftLoggerApi.Data;

public class ShiftLoggerContext : DbContext
{
    public ShiftLoggerContext(DbContextOptions<ShiftLoggerContext> options) : base(options)
    {
    }

    public DbSet<Shift> Shifts { get; set; }
}
