using Microsoft.EntityFrameworkCore;
using ShiftWebApi.Data;
using ShiftWebApi.Services;

namespace ShiftWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ShiftDbContext>(opt => opt.UseSqlServer("Data Source=localhost;Initial Catalog=ShiftsLoggerbyP13;Integrated Security=True;TrustServerCertificate=True;"));
            builder.Services.AddScoped<IShiftService, ShiftService>();
            builder.Services.AddScoped<IUserService, UserService>();
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            app.Run();
        }
    }
}
