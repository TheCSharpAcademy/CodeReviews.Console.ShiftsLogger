using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class ShiftContext : DbContext
    {
        public ShiftContext(DbContextOptions<ShiftContext> options) : base(options)
        {
        }

        public DbSet<Shift> TodoItems { get; set; } = null!;
    }
}
