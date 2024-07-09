using Microsoft.EntityFrameworkCore;
using ShiftLoggerAPI.Models;

namespace ShiftLoggerAPI.Data;

public class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Shift> Shifts { get; set; }
}