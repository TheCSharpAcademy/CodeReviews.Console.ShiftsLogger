using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ShiftLoggerContext>(opt => 
opt.UseSqlServer(builder.Configuration.GetConnectionString("ShiftContext")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var db = app.Services.CreateScope())
{
    var services = db.ServiceProvider;
    var context = services.GetRequiredService<ShiftLoggerContext>();
    Console.WriteLine("Database created!");
}

app.Run();


/*Tutorials

https://learn.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-7.0&tabs=visual-studio


*/