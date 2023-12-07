using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShiftsLogger.Models;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("https://localhost:7009");

// Add services to the container.
builder.Services.AddControllers();

// Configure the application configuration
builder.Configuration.AddJsonFile("appsettings.json");

// Configure DbContext using dependency injection
builder.Services.AddDbContext<ShiftContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnectionString")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
