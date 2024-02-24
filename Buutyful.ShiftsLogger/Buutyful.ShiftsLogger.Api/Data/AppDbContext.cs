using Buutyful.ShiftsLogger.Domain;
using Microsoft.EntityFrameworkCore;

namespace Buutyful.ShiftsLogger.Api.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Worker> Workers { get; set; }

}
