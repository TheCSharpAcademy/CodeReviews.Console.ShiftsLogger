using Microsoft.EntityFrameworkCore;
using ShiftsLogger.API.Model;

namespace ShiftsLogger.API.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options)
    : base(options)
    {
    }

    public DbSet<Shift> Shifts { get; set; }
}
