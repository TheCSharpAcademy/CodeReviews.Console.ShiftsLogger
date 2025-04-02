using ShiftsLoggerUI.Menus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        })
        .ConfigureServices((_, services) =>
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddLogging(c => c.ClearProviders());
            services.AddSingleton<HttpClient>();
            services.AddMenuServices();
            services.AddConsoleControllers();
            services.AddConsoleServices();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
        }).Build();

var mainMenu = host.Services.GetRequiredService<MainMenu>();
await mainMenu.ShowMenuAsync();

