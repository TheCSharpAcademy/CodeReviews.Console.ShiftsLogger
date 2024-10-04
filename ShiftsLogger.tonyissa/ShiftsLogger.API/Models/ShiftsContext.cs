using Microsoft.EntityFrameworkCore;

namespace ShiftsLogger.API.Models;

public class ShiftsContext : DbContext
{
    public DbSet<ShiftsEntry> ShiftsEntry { get; set; }

    private readonly string DbPath;

    public ShiftsContext(IConfiguration configuration)
    {
        DbPath = configuration.GetConnectionString("DefaultConnection")!;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer($"Server={DbPath}");
}