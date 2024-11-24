using Microsoft.EntityFrameworkCore;
using ShiftsLoggerApi.Controllers;

namespace ShiftsLoggerApi.Factory
{
    internal class HostFactory
    {
        public static IHost CreateDbHost(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    var configuration = context.Configuration;

                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

                    services.AddControllers();
                    services.AddEndpointsApiExplorer();
                    services.AddSwaggerGen();


                }).Build();
        }

    }
}