using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShiftsLogger.App.App;
using ShiftsLogger.App.Client;
using ShiftsLogger.App.Services;
using ShiftsLogger.App.Services.Interfaces;


var host = Host.CreateDefaultBuilder(args);

host.ConfigureServices(services =>
{

    services.AddHttpClient<ShiftsApiClient>(client =>
    {
        client.BaseAddress = new Uri("https://localhost:52036");
    });
    services.AddSingleton<IUserInputValidationService, UserInputValidationService>();
    services.AddSingleton<ShiftsLoggerApp>();
});

host.ConfigureLogging(logger =>
{
    logger.ClearProviders();
    logger.AddDebug();
    logger.AddConsole();
});


var app = host.Build();

await app.Services.GetRequiredService<ShiftsLoggerApp>().RunApp();
