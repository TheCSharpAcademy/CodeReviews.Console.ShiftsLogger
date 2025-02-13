using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Backend.Data.Model;

namespace ShiftsLogger.Backend.Data;

public class ShiftDbContext: DbContext
{
    public ShiftDbContext(DbContextOptions options) : base(options)
    {
    }


    public DbSet<Shift> ShiftsTable { get; set; }
}
