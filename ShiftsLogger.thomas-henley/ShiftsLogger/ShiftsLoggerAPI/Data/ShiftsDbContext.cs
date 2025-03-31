using Microsoft.EntityFrameworkCore;
using ShiftsLoggerModels;

namespace ShiftsLoggerAPI.Data;

public class ShiftsDbContext(DbContextOptions<ShiftsDbContext> options) : DbContext(options)
{
    public DbSet<Shift> Shifts { get; set; }
}