using ShiftsLoggerWebApi.Models;


namespace ShiftsLoggerWebApi;

public class ShiftsLoggerWebApi
{
    public static void Main()
    {
        Helpers.JsonConfig();
        bool dbInitSuccesful = false;
        try
        {
            dbInitSuccesful = Helpers.DBInit();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        if (dbInitSuccesful)
            WebApiBuilder();
    }

    public static void WebApiBuilder()
    {
        var builder = WebApplication.CreateBuilder();
        
        builder.Services.AddControllers().AddNewtonsoftJson();
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
}