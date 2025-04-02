using ShiftsLoggerUI.Controllers;
using ShiftsLoggerUI.Services;
using ShiftsLoggerUI.Menus;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConsoleServiceExtensions
{
    public static IServiceCollection AddMenuServices(this IServiceCollection services)
    {
        services.AddScoped<MainMenu>();
        services.AddScoped<EmployeeMenu>();
        services.AddScoped<ShiftsMenu>();

        return services;
    }

    public static IServiceCollection AddConsoleControllers(this IServiceCollection services)
    {
        services.AddScoped<EmployeeController>();
        services.AddScoped<ShiftController>();

        return services;
    }

    public static IServiceCollection AddConsoleServices(this IServiceCollection services)
    {
        services.AddScoped<EmployeesService>();
        services.AddScoped<SeedingService>();
        services.AddScoped<ShiftsService>();
        services.AddScoped<TableVisualisationEngine<object>>();

        return services;
    }
}
