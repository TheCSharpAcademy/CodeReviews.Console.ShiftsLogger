using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShiftsLoggerApi.DataAccess;
using ShiftsLoggerApi.Services;

namespace ShiftsLoggerApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        
        // Configure DbContext with the connection string from appsettings.json
        builder.Services.AddDbContext<ShiftContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
        builder.Services.AddScoped<IShiftService, ShiftService>();
        
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
    }
}