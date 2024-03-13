using Microsoft.EntityFrameworkCore;
using ShiftsLoggerWebAPI.Models;
using ShiftsLoggerWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ShiftContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddScoped<IShiftService, ShiftService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
