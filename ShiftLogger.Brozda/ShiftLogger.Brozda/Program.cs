using ShiftLogger.Brozda.API.Data;
using ShiftLogger.Brozda.API.Endpoints;
using ShiftLogger.Brozda.API.Repositories;

namespace ShiftLogger.Brozda.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ShiftLoggerContext>();
            builder.Services.AddScoped<IWorkerRepository, WorkerRepository>();
            builder.Services.AddScoped<IShiftTypeRepository, ShiftTypeRepository>();
            builder.Services.AddScoped<IAssignedShiftRepository, AssignedShiftRepository>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapWorkerEndpoints();
            app.MapShiftTypeEndpoints();
            app.MapAssignedShiftsEndpoints();

            app.MapControllers();
            app.Run();
        }
    }
}