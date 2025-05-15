using ShiftsLogger.WebApi.Data;
using ShiftsLogger.WebApi.Filters;
using ShiftsLogger.WebApi.Repository;

namespace ShiftsLogger.WebApi;

class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<WorkerShiftContext>();
        builder.Services.AddScoped<WorkerShiftRepository>();
        builder.Services.AddScoped<ManagerAuthorizationFilter>();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<WorkerShiftContext>();

            await DbInitializer.Seed(context);
        }

        app.MapOpenApi();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Shifts Logger v1 API");
        });

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}