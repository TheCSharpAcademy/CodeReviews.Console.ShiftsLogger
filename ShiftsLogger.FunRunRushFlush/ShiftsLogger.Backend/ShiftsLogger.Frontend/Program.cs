using ShiftsLogger.Frontend.App;
using ShiftsLogger.Frontend.Client;
using ShiftsLogger.Frontend.Services;
using ShiftsLogger.Frontend.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

 builder.Services.AddHttpClient<ShiftsApiClient>(client =>
 {
     // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
     // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
     client.BaseAddress = new Uri("https://localhost:52036");
 });


builder.Services.AddSingleton<IUserInputValidationService, UserInputValidationService>();
builder.Services.AddSingleton<ShiftsLoggerApp>();


//builder.AddServiceDefaults();

var app = builder.Build();

await app.Services.GetRequiredService<ShiftsLoggerApp>().RunApp();
