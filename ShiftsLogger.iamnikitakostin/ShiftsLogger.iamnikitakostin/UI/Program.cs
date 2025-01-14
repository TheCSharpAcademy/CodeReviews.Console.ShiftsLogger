using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UI.Controllers;
using UI.Interfaces;
using UI.Services;
using UI;

internal class Program : ConsoleController
{
    static async Task Main()
    {

        try
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        string? connectionString = config.GetConnectionString("DefaultConnection");

        if (connectionString == null)
        {
            ErrorMessage("Dear user, please ensure that you have your api connection string set up in the appsettings.json.");
            return;
        }

        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(config);

        services.AddScoped<IShiftService, ShiftService>();
        services.AddScoped<IWorkerService, WorkerService>();
        services.AddScoped<View>();

        var serviceProvider = services.BuildServiceProvider();

        var userInterface = serviceProvider.GetRequiredService<View>();
        await userInterface.ShowMainMenu();
        } catch (Exception ex)
        {
            ErrorMessage(ex.Message);
        }
    }
}