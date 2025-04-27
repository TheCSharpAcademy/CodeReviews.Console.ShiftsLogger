using Microsoft.EntityFrameworkCore;
using ShiftsLogger.SpyrosZoupas.DAL;
using ShiftsLogger.SpyrosZoupas.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding services like this basically tells the program "If a class constructor is trying to inject X service, then create a Scoped/Singleton etc. object of X service and inject it into that class
builder.Services.AddDbContext<ShiftsLoggerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IShiftService, ShiftService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // what is this?

app.MapControllers();

app.Run();
