using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Dejmenek.UI;
using ShiftsLogger.Dejmenek.UI.Controllers;
using ShiftsLogger.Dejmenek.UI.Data.Interfaces;
using ShiftsLogger.Dejmenek.UI.Data.Repositories;
using ShiftsLogger.Dejmenek.UI.Services;
using ShiftsLogger.Dejmenek.UI.Services.Interfaces;

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var services = new ServiceCollection();

services.AddSingleton(configuration);
services.AddSingleton<HttpClient>();
services.AddScoped<IUserInteractionService, UserInteractionService>();
services.AddScoped<Menu>();
services.AddScoped<IEmployeeService, EmployeeService>();
services.AddScoped<IShiftService, ShiftService>();
services.AddScoped<IEmployeesRepository, EmployeesRepository>();
services.AddScoped<IShiftsRepository, ShiftsRepository>();
services.AddTransient<ShiftsController>();
services.AddTransient<EmployeesController>();

var serviceProvider = services.BuildServiceProvider();

var start = serviceProvider.GetService<Menu>();

await start.Run();