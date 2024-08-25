using Microsoft.EntityFrameworkCore;

namespace ShiftsLoggerAPI
{
    public class ShiftDbContext : DbContext
    {
        public ShiftDbContext(DbContextOptions<ShiftDbContext> options) : base(options) { }
        public DbSet<Shift> Shifts { get; set; }
    }
}
