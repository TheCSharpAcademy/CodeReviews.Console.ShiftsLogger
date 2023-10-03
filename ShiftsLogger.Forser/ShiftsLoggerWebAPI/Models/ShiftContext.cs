using Microsoft.EntityFrameworkCore;

namespace ShiftsLoggerWebAPI.Models
{
    public class ShiftContext : DbContext
    {
        public ShiftContext() { }
        public ShiftContext(DbContextOptions<ShiftContext> options) : base(options) { }
        public DbSet<ShiftModel> Shifts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\;Database=ShiftsLogger;Trusted_Connection=True;MultipleActiveResultSets=True;Encrypt=False");
        }
    }
}