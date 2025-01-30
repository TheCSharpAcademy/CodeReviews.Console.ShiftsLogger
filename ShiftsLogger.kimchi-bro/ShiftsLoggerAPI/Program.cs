using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Helpers;
using Scalar.AspNetCore;
using ShiftsLoggerAPI.Data;

namespace ShiftsLoggerAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<ShiftsLoggerDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        var app = builder.Build();

        DbHelper.CheckDb(app);

        if (app.Environment.IsDevelopment())
        {
            app.MapScalarApiReference();
            app.MapOpenApi();
        }

        app.MapControllers();

        app.Run();
    }
}
