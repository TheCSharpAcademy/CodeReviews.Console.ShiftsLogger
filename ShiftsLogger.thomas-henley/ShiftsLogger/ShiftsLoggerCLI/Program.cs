using Microsoft.Extensions.Configuration;
using ShiftsLoggerClientLibrary.ApiClients;
using ShiftsLoggerCLI;

var builder = new ConfigurationBuilder();
builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
var config = builder.Build();

if (!int.TryParse(config["Port"], out var port))
{
    port = 7101;
}

IShiftApiClient client = new ShiftsHttpClient(port);

var controller = new SpectreController(client);

controller.Run();