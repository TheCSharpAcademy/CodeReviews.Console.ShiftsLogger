using ShiftLoggerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ShiftLoggerAPI.DataAccess
{
    public class ShiftLoggerContext : DbContext
    {
        public DbSet<ShiftLogger> Shifts { get; set; }
        public ShiftLoggerContext(DbContextOptions<ShiftLoggerContext> options) : base(options)
        {
        }
    }
}
