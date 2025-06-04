using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Refit;
using ShiftsLogger.ConsoleUI.Menus;
using ShiftsLogger.ConsoleUI.RefitClients;

using IHost host = Host.CreateDefaultBuilder(args)
		.ConfigureServices((_, services) =>
		{
			services.AddLogging(c => c.ClearProviders());
			services.AddRefitClient<IShiftsLoggerClient>()
			.ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7045/api"));
			services.AddMenuServices();
			services.AddConsoleServices();
			services.AddConsoleControllers();

		}).Build();

var mainMenu = host.Services.GetRequiredService<MainMenu>();

await mainMenu.DisplayAsync();