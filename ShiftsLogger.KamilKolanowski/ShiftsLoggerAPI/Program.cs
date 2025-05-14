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
        builder.Services.AddScoped<WorkerApi>();

        var app = builder.Build();

        var api = app.Services.GetRequiredService<WorkerApi>();
        api.Configure(app);

        await app.RunAsync();
    }
}
