using Microsoft.EntityFrameworkCore;

namespace ShiftsLogger.Models
{
    public class ShiftContext : DbContext
    {
        public DbSet<ShiftModel> Shifts { get; set; } = null!;

        public ShiftContext(DbContextOptions<ShiftContext> options) : base(options) { }
    }
}
