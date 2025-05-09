using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerV2.RyanW84.Models;

namespace ShiftsLoggerV2.RyanW84.Data;

public class ShiftsDbContext : DbContext
{
    public ShiftsDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Shift> Shifts { get; set; }
}
