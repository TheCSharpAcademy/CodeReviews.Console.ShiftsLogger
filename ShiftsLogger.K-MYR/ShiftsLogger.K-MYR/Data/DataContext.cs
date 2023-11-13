using Microsoft.EntityFrameworkCore;

namespace ShiftsLogger.K_MYR;

public class DataContext : DbContext
{
    private readonly string _connectionString = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder 
    {
        DataSource = "",
        InitialCatalog = "",
        IntegratedSecurity = true
    }.ConnectionString;

    public DbSet<Shift> Shifts {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }

}
