using Microsoft.EntityFrameworkCore;
using ShiftLoggerApi.Models;

namespace ShiftLoggerApi.DataContext;

public class ShiftContext : DbContext
{
    public DbSet<Shift> Shifts { get; set; }

    public ShiftContext(DbContextOptions<ShiftContext> options) : base(options)
    {
    }
}