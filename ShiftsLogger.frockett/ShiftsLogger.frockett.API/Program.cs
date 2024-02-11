using Microsoft.EntityFrameworkCore;
using ShiftsLogger.frockett.API.Data;
using ShiftsLogger.frockett.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IShiftsRepository, ShiftsRepository>();

builder.Services.AddDbContext<ShiftLogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.Run();
