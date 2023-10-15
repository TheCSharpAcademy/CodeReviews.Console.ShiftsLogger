using Microsoft.EntityFrameworkCore;

namespace ShiftLoggerAPI.Models
{
    public class ShiftContext : DbContext
    {
        protected ShiftContext()
        {
        }

        public ShiftContext(DbContextOptions options) : base(options)
        {
           
        }

        public DbSet<Shift> Shifts { get; set; } = null!;
    }
}
