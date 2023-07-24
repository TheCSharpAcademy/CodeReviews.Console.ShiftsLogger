using Microsoft.EntityFrameworkCore;

namespace ShiftLogger.JsPeanut.Models
{
    public class ShiftContext : DbContext
    {
        public ShiftContext(DbContextOptions<ShiftContext> options) 
            : base(options)
        {
        }

        public DbSet<Shift> Shifts { get; set; } = null!;
    }
}
