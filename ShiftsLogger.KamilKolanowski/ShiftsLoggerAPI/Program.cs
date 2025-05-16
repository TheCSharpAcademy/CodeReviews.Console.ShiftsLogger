using Microsoft.EntityFrameworkCore;
using ShiftsLogger.KamilKolanowski.Models.Data;
using ShiftsLoggerAPI.Services;

namespace ShiftsLoggerAPI;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<ShiftsLoggerDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        builder.Services.AddScoped<WorkerDbService>();
        builder.Services.AddScoped<ShiftDbService>();
        builder.Services.AddScoped<WorkerApi>();
        builder.Services.AddScoped<ShiftApi>();

        var app = builder.Build();

        var workerApi = app.Services.GetRequiredService<WorkerApi>();
        var shiftApi = app.Services.GetRequiredService<ShiftApi>();

        workerApi.Configure(app);
        shiftApi.Configure(app);

        await app.RunAsync();
    }
}
