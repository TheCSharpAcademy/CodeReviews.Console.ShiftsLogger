using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ShiftsLoggerClient.Coordinators;
using ShiftsLoggerClient.Display;
using ShiftsLoggerClient.Services;
using ShiftsLoggerClient.UserInterface;
using ShiftsLoggerClient.Utilities;

namespace ShiftsLoggerClient;

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();

        services.AddSingleton<UserInput>();
        services.AddSingleton<EmployeeService>();
        services.AddSingleton<ShiftService>();
        services.AddSingleton<CalculateDuration>();
        services.AddSingleton<Validation>();
        services.AddSingleton<DateTimeParser>();
        services.AddSingleton<DisplayManager>();
        services.AddSingleton<AppCoordinator>();

        var serviceProvider = services.BuildServiceProvider();

        await serviceProvider.GetRequiredService<AppCoordinator>().Start();
    }
}




