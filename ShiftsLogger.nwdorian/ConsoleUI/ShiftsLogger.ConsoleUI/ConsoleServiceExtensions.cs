using ShiftsLogger.ConsoleUI.Controllers;
using ShiftsLogger.ConsoleUI.Menus;
using ShiftsLogger.ConsoleUI.Services;

namespace Microsoft.Extensions.DependencyInjection;
public static class ConsoleServiceExtensions
{
	public static IServiceCollection AddMenuServices(this IServiceCollection services)
	{
		services.AddScoped<MainMenu>();
		services.AddScoped<UsersMenu>();
		services.AddScoped<ShiftsMenu>();
		services.AddScoped<SeedingMenu>();
		return services;
	}

	public static IServiceCollection AddConsoleServices(this IServiceCollection services)
	{
		services.AddScoped<SeedingService>();
		services.AddScoped<UsersService>();
		services.AddScoped<ShiftsService>();
		return services;
	}

	public static IServiceCollection AddConsoleControllers(this IServiceCollection services)
	{
		services.AddScoped<UsersController>();
		services.AddScoped<ShiftsController>();
		return services;
	}
}
