using Kmakai.ShiftsLogger.Models;
using Microsoft.EntityFrameworkCore;

namespace Kmakai.ShiftsLogger.DataAccess;

public class ShiftsLoggerContext: DbContext
{
    public ShiftsLoggerContext(DbContextOptions<ShiftsLoggerContext> options) : base(options)
    {
    }

    public DbSet<Shift> Shifts { get; set; } = null!;
}
