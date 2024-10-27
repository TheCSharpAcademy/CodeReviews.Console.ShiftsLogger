using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI;
using ShiftsLoggerAPI.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Configure Entity Framework with your DbContext
builder.Services.AddDbContext<ShiftsLoggerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IWorkerService, WorkerService>();
builder.Services.AddScoped<IShiftsService, ShiftsService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
/*using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ShiftsLoggerContext>();
    dbContext.Database.Migrate();
}*/
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();