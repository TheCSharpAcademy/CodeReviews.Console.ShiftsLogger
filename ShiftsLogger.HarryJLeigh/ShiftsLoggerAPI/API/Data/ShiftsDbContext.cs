using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace ShiftsLogger.API.Data;

public class ShiftsDbContext(DbContextOptions<ShiftsDbContext> options) : DbContext(options)
{
    public DbSet<Shift> Shifts { get; set; }
}