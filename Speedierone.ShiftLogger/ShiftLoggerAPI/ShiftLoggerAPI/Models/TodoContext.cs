using Microsoft.EntityFrameworkCore;

namespace ShiftLoggerAPI.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) 
        {            
        }
        public DbSet<ShiftTimes> ShiftTimes { get; set; } = null!;
    }
}
