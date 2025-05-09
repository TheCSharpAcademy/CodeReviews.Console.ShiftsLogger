using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ShiftsLoggerV2.RyanW84.Data;

public class ShiftsDbContext : DbContext
{
    public ShiftsDbContext(DbContextOptions options)
        : base(options) { }
}
