using Microsoft.EntityFrameworkCore;

namespace ShiftLogger.Cactus.DataModel;

public class ShiftLoggerContext : DbContext
{
    public ShiftLoggerContext(DbContextOptions<ShiftLoggerContext> options)
    : base(options)
    {
    }

    public DbSet<ShiftLogger> ShiftLoggers { get; set; } = null!;
}

