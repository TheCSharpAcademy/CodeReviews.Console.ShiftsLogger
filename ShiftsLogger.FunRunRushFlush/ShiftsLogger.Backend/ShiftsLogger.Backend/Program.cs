using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShiftsLogger.Backend.Data;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<ShiftDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseSqlServer(context.Configuration.GetConnectionString("SQLServerConnection"));
        });


    })
    .ConfigureLogging(logger =>
    {
        logger.AddDebug();
        logger.AddConsole();
    }).Build();