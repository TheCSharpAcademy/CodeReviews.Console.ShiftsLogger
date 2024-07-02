using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShiftLoggerConsole;
using ShiftLoggerConsole.Controller;
using ShiftLoggerConsole.Services;
using ShiftLoggerConsole.TableVisualization;
using ShiftLoggerConsole.UserInput;
using ShiftLoggerConsole.Validation;

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)!.FullName)
    .AddJsonFile("appsettings.json")
    .Build();

var services = new ServiceCollection();

services.AddSingleton(configuration);
services.AddSingleton<IStartup, Startup>();
services.AddSingleton<HttpClient>();
services.AddTransient<IApiConnectionService, ApiConnectionService>();
services.AddTransient<IShiftController, ShiftController>();
services.AddTransient<IInput, Input>();
services.AddTransient<IInputValidator, InputValidator>();
services.AddSingleton<Menus>();
services.AddTransient<ITableBuilder, TableBuilder>();

var serviceProvider = services.BuildServiceProvider();
var startup = serviceProvider.GetService<IStartup>();

await startup!.Run();