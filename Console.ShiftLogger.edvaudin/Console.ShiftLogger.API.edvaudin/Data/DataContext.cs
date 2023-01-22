using Microsoft.EntityFrameworkCore;
using ShiftLogger.API.Models;

namespace ShiftLogger.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shift> Shifts { get; set; }
    }
}
