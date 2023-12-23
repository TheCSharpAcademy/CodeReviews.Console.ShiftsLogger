using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Models;
using ShiftsLoggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ShiftContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnectionString"));
});
builder.Services.AddScoped<ShiftContext>();

var app = builder.Build();

// Use the existing ShiftContext instance registered in the web project
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ShiftContext>();

    // Create the ShiftsLoggerService and MenuBuilders with dependencies
    ShiftsLoggerService shiftsLoggerService = new ShiftsLoggerService(dbContext);
    MenuBuilders menuBuilder = new MenuBuilders(shiftsLoggerService);

    // Call the MainMenu method to start the application
    menuBuilder.MainMenu();
}

Console.ReadLine();