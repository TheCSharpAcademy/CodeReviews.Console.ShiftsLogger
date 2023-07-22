using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ShiftsLogger.Model;

public class ShiftLoggerContext : DbContext
{

    public DbSet<ShiftItem> ShiftItems { get; set; }
    public DbSet<Employee> Employees { get; set; }

    public ShiftLoggerContext( DbContextOptions<ShiftLoggerContext> options ) : base(options) { }

    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    {
        SqlConnectionStringBuilder builder = new();

        builder.DataSource = "(localdb)\\mssqllocaldb";
        builder.InitialCatalog = "ShiftControl";
        builder.IntegratedSecurity = true;
        builder.TrustServerCertificate = true;
        builder.MultipleActiveResultSets = true;
        builder.ConnectTimeout = 3;

        string? connection = builder.ConnectionString;
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.Entity<ShiftItem>().ToTable("ShiftItem");
    }
}
