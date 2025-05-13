using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Scalar.AspNetCore;
using ShiftsLoggerV2.RyanW84.Data;
using ShiftsLoggerV2.RyanW84.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// Prevents circular dependency issues
builder
    .Services.AddControllers()
    .AddJsonOptions(opts =>
        opts.JsonSerializerOptions.ReferenceHandler = System
            .Text
            .Json
            .Serialization
            .ReferenceHandler
            .IgnoreCycles
    );

builder.Services.AddDbContext<ShiftsDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<IShiftService, ShiftService>(); //Implementing the service in the DI container
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    Console.WriteLine("Development Mode");

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ShiftsDbContext>();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
    dbContext.SeedData();
    Console.WriteLine("Database seeded");
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
