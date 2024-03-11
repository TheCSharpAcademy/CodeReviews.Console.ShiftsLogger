using System.Globalization;
using WebApplication1.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WorkerContext>();

CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("v1/swagger.json", "V1 Docs");
    });
}

app.UseAuthorization();
app.MapControllers();
app.Run();
