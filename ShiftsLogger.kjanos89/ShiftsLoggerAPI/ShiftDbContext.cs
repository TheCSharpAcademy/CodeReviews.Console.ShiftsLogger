using Microsoft.EntityFrameworkCore;

namespace ShiftsLoggerAPI
{
    public class ShiftDbContext : DbContext
    {
        public ShiftDbContext(DbContextOptions<ShiftDbContext> options) : base(options) { }

        public DbSet<Worker> Workers { get; set; }
        public DbSet<Shift> Shifts { get; set; }
    }
}
