using Microsoft.EntityFrameworkCore;
using ShiftLogger.Mefdev.ShiftLoggerAPI.Models;
using ShiftLogger.Mefdev.ShiftLoggerAPI.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<WorkerShiftContext>(opt=> opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// IOC
builder.Services.AddScoped<WorkerShiftService>();
builder.Services.AddScoped<WorkerShiftMapper>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();


