using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Shiftlogger.Data;

public class ShiftloggerDbContextFactory : IDesignTimeDbContextFactory<ShiftloggerDbContext>
{
    public ShiftloggerDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder().AddUserSecrets<ShiftloggerDbContextFactory>()
            .Build();
        var optionsBuilder = new DbContextOptionsBuilder<ShiftloggerDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);

        return new ShiftloggerDbContext(optionsBuilder.Options);
    }
}