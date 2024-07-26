using Microsoft.EntityFrameworkCore;
namespace ShiftsLogger.ukpagrace.Models
{
    public class ShiftLogContext: DbContext
    {
        public ShiftLogContext(DbContextOptions<ShiftLogContext> options) : base(options) 
        { 

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Implemented in controller
        }
           
        public DbSet<ShiftLog> ShiftLog { get; set; } = null!;
    }
}
