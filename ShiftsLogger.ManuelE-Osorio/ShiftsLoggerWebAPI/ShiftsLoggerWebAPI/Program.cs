using System.Data.Common;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using ShiftsLoggerWebApi.Models;

namespace ShiftsLogger;

public class ShiftsLoggerWebAPI
{
    public static void Main()
    {
        JsonConfig();
        DBInit();
        // PopulateDB();
        WebAPIBuilder();
    }

    public static void WebAPIBuilder()
    {
        var builder = WebApplication.CreateBuilder();
        
        builder.Services.AddControllers();
        builder.Services.AddDbContext<ShiftsLoggerContext>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();

        if(app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }

    public static void JsonConfig()
    {
        IConfiguration jsonConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        ShiftsLoggerContext.ShiftsLoggerConnectionString = jsonConfig.GetConnectionString("DefaultConnection");
    }

    public static void PopulateDB()
    {
        var db = new ShiftsLoggerContext();
        db.Employees.Add(new Employee{Name = "Employee1"});
        db.Employees.Add(new Employee{Name = "Employee2"});
        db.Employees.Add(new Employee{Name = "Employee3"});
        db.Employees.Add(new Employee{Name = "Employee4"});
        db.Employees.Add(new Employee{Name = "Employee5"});
        db.Employees.Add(new Employee{Name = "Employee6"});
        db.Employees.Add(new Employee{Name = "Employee7"});
        db.Employees.Add(new Employee{Name = "Employee8"});
        db.Employees.Add(new Employee{Name = "Employee9"});
        db.Employees.Add(new Employee{Name = "Employee10"});
        db.Employees.Add(new Employee{Name = "Employee11"});
        db.Employees.Add(new Employee{Name = "Employee12"});
        db.Employees.Add(new Employee{Name = "Employee13"});
        db.Employees.Add(new Employee{Name = "Employee14"});
        db.Employees.Add(new Employee{Name = "Employee15"});
        db.Employees.Add(new Employee{Name = "Employee16"});
        db.Employees.Add(new Employee{Name = "Employee17"});
        db.Employees.Add(new Employee{Name = "Employee18"});
        db.Employees.Add(new Employee{Name = "Employee19"});
        db.Employees.Add(new Employee{Name = "Employee20"});                
        db.SaveChanges();

        var employee = db.Employees.Where(p => p.Name == "Employee1").First();
        var start = new DateTime(2024, 1, 20);
        employee.Shifts?.Add(new Shift{ShiftStartTime = start.ToUniversalTime(), ShiftEndTime = DateTime.UtcNow} );

         start = new DateTime(2024, 1, 19);
        employee.Shifts?.Add(new Shift{ShiftStartTime = start.ToUniversalTime(), 
            ShiftEndTime = new DateTime(2024, 1 ,20).ToUniversalTime()} );

        employee = db.Employees.Where(p => p.Name == "Employee3").First();
         start = new DateTime(2024, 1, 20);
        employee.Shifts?.Add(new Shift{ShiftStartTime = start.ToUniversalTime(), ShiftEndTime = DateTime.UtcNow} );

        employee = db.Employees.Where(p => p.Name == "Employee5").First();
         start = new DateTime(2024, 1, 20);
        employee.Shifts?.Add(new Shift{ShiftStartTime = start.ToUniversalTime(), ShiftEndTime = DateTime.UtcNow} );

        employee = db.Employees.Where(p => p.Name == "Employee7").First();
         start = new DateTime(2024, 1, 20);
        employee.Shifts?.Add(new Shift{ShiftStartTime = start.ToUniversalTime(), ShiftEndTime = DateTime.UtcNow} );    

        employee = db.Employees.Where(p => p.Name == "Employee9").First();
         start = new DateTime(2024, 1, 20);
        employee.Shifts?.Add(new Shift{ShiftStartTime = start.ToUniversalTime(), ShiftEndTime = DateTime.UtcNow} );

        employee = db.Employees.Where(p => p.Name == "Employee11").First();
         start = new DateTime(2024, 1, 20);
        employee.Shifts?.Add(new Shift{ShiftStartTime = start.ToUniversalTime(), ShiftEndTime = DateTime.UtcNow} );

        employee = db.Employees.Where(p => p.Name == "Employee13").First();
         start = new DateTime(2024, 1, 20);
        employee.Shifts?.Add(new Shift{ShiftStartTime = start.ToUniversalTime(), ShiftEndTime = DateTime.UtcNow} );

        employee = db.Employees.Where(p => p.Name == "Employee15").First();
         start = new DateTime(2024, 1, 20);
        employee.Shifts?.Add(new Shift{ShiftStartTime = start.ToUniversalTime(), ShiftEndTime = DateTime.UtcNow} );

        employee = db.Employees.Where(p => p.Name == "Employee17").First();
         start = new DateTime(2024, 1, 20);
        employee.Shifts?.Add(new Shift{ShiftStartTime = start.ToUniversalTime(), ShiftEndTime = DateTime.UtcNow} );    

        employee = db.Employees.Where(p => p.Name == "Employee19").First();
         start = new DateTime(2024, 1, 20);
        employee.Shifts?.Add(new Shift{ShiftStartTime = start.ToUniversalTime(), ShiftEndTime = DateTime.UtcNow} );
        
        db.SaveChanges();
    }

    public static void DBInit()
    {
        var db = new ShiftsLoggerContext();
        db.Database.EnsureCreated();
    }
}
// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();

// app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
