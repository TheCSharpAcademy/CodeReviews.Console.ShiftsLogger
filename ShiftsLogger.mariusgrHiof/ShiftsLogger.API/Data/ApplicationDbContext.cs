using Microsoft.EntityFrameworkCore;
using ShiftsLogger.API.Models;

namespace ShiftsLogger.API.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Shift> Shifts { get; set; }
}