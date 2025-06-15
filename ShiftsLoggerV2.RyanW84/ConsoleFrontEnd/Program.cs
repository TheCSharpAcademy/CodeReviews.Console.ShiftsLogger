using ConsoleFrontEnd.MenuSystem;
using ConsoleFrontEnd.Services;

using Microsoft.Extensions.DependencyInjection;

namespace ConsoleFrontEnd;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Set up DI
        var baseUri = new Uri("https://localhost:7009/");

        var services = new ServiceCollection();
        services.AddHttpClient<IWorkerService, WorkerService>(client => client.BaseAddress = baseUri);
        services.AddHttpClient<IShiftService, ShiftService>(client => client.BaseAddress = baseUri);
        services.AddHttpClient<ILocationService, LocationService>(client => client.BaseAddress = baseUri);

        var serviceProvider = services.BuildServiceProvider();

        var workerService = serviceProvider.GetRequiredService<IWorkerService>();
        var shiftService = serviceProvider.GetRequiredService<IShiftService>();
        var locationService = serviceProvider.GetRequiredService<ILocationService>();

        // Pass services to your menu system as needed
        await MainMenu.DisplayMainMenu(workerService, shiftService, locationService);
    }
}
