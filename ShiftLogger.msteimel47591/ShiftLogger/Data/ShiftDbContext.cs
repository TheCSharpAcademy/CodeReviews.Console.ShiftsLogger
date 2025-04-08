using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Data;

public class ShiftDbContext : DbContext
{
    public DbSet<Models.Shift> Shift { get; set; }

    public ShiftDbContext(DbContextOptions<ShiftDbContext> options) : base(options)
    {
    }
}

