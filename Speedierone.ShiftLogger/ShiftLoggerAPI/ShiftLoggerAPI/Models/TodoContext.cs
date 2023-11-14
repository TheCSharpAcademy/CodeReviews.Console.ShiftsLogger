using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ShiftLoggerAPI.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) 
        {        
            Database.EnsureCreated();
        }
        public DbSet<ShiftTimes> ShiftTimes { get; set; } = null!;
    }
}
