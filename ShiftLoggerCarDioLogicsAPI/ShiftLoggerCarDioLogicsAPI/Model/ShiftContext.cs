using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace ShiftLoggerCarDioLogicsAPI.Model;

public class ShiftContext : DbContext
{
    public DbSet<Shift> Shifts { get; set; } = null;

    public ShiftContext(DbContextOptions<ShiftContext> options) : base(options)
    {

    }
}