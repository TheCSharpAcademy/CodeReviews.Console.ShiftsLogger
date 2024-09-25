using Microsoft.Extensions.Configuration;
using ShiftsLoggerLibrary;
using ShiftsLoggerUI;

var builder = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

IConfiguration config = builder.Build();

CancelSetup.CancelString = config.GetValue<string>("CancelString") ?? "c";

UserInterface userInterface = new();
userInterface.Run();