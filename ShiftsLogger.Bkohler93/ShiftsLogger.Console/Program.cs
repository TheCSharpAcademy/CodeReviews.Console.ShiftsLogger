using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShiftsLogger;
using ShiftsLogger.Controllers;
using ShiftsLogger.Services;

var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<App>();
                    services.AddTransient<MainMenuController>();
                    services.AddTransient<WorkerController>();
                    services.AddTransient<ShiftController>();
                    services.AddTransient<WorkerShiftController>();
                    services.AddSingleton(sp => new ApiService(context.Configuration["ApiSettings:BaseUrl"]!));
                })
                .Build();

var app = host.Services.GetRequiredService<App>();
app.Run();