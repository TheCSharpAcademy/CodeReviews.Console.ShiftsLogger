using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using ShiftsLoggerV2.RyanW84.Data;
using ShiftsLoggerV2.RyanW84.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<ShiftsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<IShiftService, ShiftService>(); //Implementing the service in the DI container

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    Console.WriteLine("Development Mode");
}

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options
        .WithTitle("Shifts Logger API")
        .WithTheme(ScalarTheme.BluePlanet)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
        .WithModels(true)
        .WithLayout(ScalarLayout.Classic);
});

app.MapControllers();

app.Run();
