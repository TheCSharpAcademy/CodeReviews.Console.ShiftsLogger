using ShiftsLoggerApi.Employees;
using ShiftsLoggerApi.Shifts;

namespace ShiftsLoggerApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddScoped(typeof(ShiftsService));
        builder.Services.AddScoped(typeof(EmployeesService));

        builder.Services.AddDbContext<ShiftsLoggerContext>();

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