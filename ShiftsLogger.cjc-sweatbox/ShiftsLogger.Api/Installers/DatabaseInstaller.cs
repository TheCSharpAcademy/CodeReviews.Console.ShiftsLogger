using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Data.Contexts;
using ShiftsLogger.Data.Services;

namespace ShiftsLogger.Api.Installers;

/// <summary>
/// Register the required Database services to the DI container.
/// </summary>
public class DatabaseInstaller : IInstaller
{
    #region Methods

    public void InstallServices(WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        builder.Services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddScoped<IShiftService, ShiftService>();
    }

    #endregion
}
