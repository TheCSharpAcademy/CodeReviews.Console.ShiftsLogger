using Microsoft.EntityFrameworkCore;
using ShiftsLoggerApi.Controllers;

namespace ShiftsLoggerApi.Factory
{
    internal class HostFactory
    {
        public static WebApplication CreateWebApplication(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            var services = builder.Services;

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .LogTo(Console.WriteLine, LogLevel.None)
                .UseLazyLoadingProxies());
            
            services.AddRouting();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return builder.Build();

        }

    }
}