using Microsoft.EntityFrameworkCore;
using ShiftApi.Models;
using ShiftApi.Service;

namespace ShiftApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddDbContext<ShiftApiContext>(
            opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("ShiftLoggerDb"))
            );
        builder.Services.AddScoped<ShiftService>();
        builder.Services.AddScoped<EmployeeService>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();


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
