using Microsoft.EntityFrameworkCore;
using ShiftsLoggerApi.Models;

namespace ShiftsLoggerApi.DataAccess;

public class ShiftContext : DbContext
{
    public ShiftContext(DbContextOptions<ShiftContext> options) : base(options) { }

    public DbSet<ShiftModel> ShiftModels { get; set; } = null!;
}