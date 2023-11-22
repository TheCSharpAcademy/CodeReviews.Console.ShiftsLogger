using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ShiftsLogger.K_MYR;

public class DataContext : IdentityDbContext<ApplicationUser>
{
    private readonly string _connectionString = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder
    {
        DataSource = System.Configuration.ConfigurationManager.AppSettings.Get("ServerName"),
        InitialCatalog = System.Configuration.ConfigurationManager.AppSettings.Get("DatabaseName"),
        IntegratedSecurity = true,
        TrustServerCertificate = true,
        Encrypt = true
    }.ConnectionString;

    public DbSet<Shift> Shifts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shift>()
            .HasOne(s => s.ApplicationUser)
            .WithMany(u => u.Shifts)
            .HasForeignKey(s => s.UserId);

        base.OnModelCreating(modelBuilder);
    }
}
