using Microsoft.EntityFrameworkCore;
using ShiftApi.Models;
using ShiftApi.Service;

namespace ShiftApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
            //.AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            //    options.JsonSerializerOptions.WriteIndented = true; // For better readability. Optional.
            //})
        builder.Services.AddDbContext<ShiftApiContext>(
            opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("ShiftLoggerDb"))
            );
        builder.Services.AddScoped<ShiftService>();
        builder.Services.AddScoped<EmployeeService>();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
