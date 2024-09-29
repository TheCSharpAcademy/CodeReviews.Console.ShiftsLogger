using Microsoft.EntityFrameworkCore;
using ShiftLogger.Api.Data;

var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();

var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
// Add services to the container.
builder.Services.AddControllers();
//Adds connecton string to the DbContext

builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseSqlServer(connectionString);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(option => 
    {
        option.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        option.RoutePrefix = "";
    });
}

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();
