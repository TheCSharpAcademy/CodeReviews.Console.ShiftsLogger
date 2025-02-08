using Microsoft.EntityFrameworkCore;
using ShiftsLoggerWebAPI.Data;
using ShiftsLoggerWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
string connectionString = builder.Configuration.GetConnectionString("ShiftLoggerConnection");
builder.Services.AddDbContext<ShiftsDbContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IShiftService, ShiftService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();
