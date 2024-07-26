using Microsoft.EntityFrameworkCore;
using ShiftsLogger.ukpagrace.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ShiftLogContext>(opt =>
    opt.UseSqlServer("Data Source=localhost;Initial Catalog=ShiftLogger;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.Run();
