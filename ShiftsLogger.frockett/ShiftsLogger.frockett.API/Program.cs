using Microsoft.EntityFrameworkCore;
using ShiftsLogger.frockett.API.Data;
using ShiftsLogger.frockett.API.Repositories;
using ShiftsLogger.frockett.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IShiftsRepository, ShiftsRepository>();
builder.Services.AddScoped<ShiftService>();
builder.Services.AddScoped<EmployeeService>();

builder.Services.AddDbContext<ShiftLogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShiftsLogger API");
        c.RoutePrefix = string.Empty;
    });
}

app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();
