using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ShiftsLogger.Backend.Data;
using ShiftsLogger.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContextFactory<ShiftDbContext>(optionsBuilder =>
{
    optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"));
});
builder.Services.AddSingleton<IRepository<Shift>, Repository<Shift>>();



// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.MapGet("/", () => Results.Redirect("/scalar/v1"));
}
app.MapControllers();


app.Run();
