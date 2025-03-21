using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.UI.Menu;
using ShiftsLogger.UI.Service;

ServiceCollection services = new();

services.AddHttpClient<ShiftApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7179/");
});
services.AddTransient<Menu>();

ServiceProvider serviceProvider = services.BuildServiceProvider();

Menu menu = serviceProvider.GetRequiredService<Menu>();
await menu.ShowMenu();