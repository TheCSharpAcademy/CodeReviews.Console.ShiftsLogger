using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Data.Entities;

namespace ShiftsLogger.Data.Contexts;

/// <summary>
/// The context for a database connection.
/// </summary>
public class DatabaseContext : DbContext
{
    #region Constructors

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    #endregion
    #region Properties

    public DbSet<Shift> Shift { get; set; } = null!;

    #endregion
}
