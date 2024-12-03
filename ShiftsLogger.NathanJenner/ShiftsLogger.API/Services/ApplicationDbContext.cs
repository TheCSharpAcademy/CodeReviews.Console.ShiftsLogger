using Microsoft.EntityFrameworkCore;
using ShiftsLogger.API.Models;

namespace ShiftsLogger.API.Services;

public class ApplicationDbContext : DbContext
{
    public DbSet<Shift> Shifts { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}
