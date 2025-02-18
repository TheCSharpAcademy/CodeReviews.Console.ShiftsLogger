using ShiftLoggerApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace ShiftLoggerApi;

internal class ShiftLoggerContext: DbContext
{
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Department> Departments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("ShiftLoggerDatabase");

        optionsBuilder.UseSqlServer(connectionString)
                      .ConfigureWarnings(warnings =>
                          warnings.Ignore(RelationalEventId.NonTransactionalMigrationOperationWarning));
    }
}
