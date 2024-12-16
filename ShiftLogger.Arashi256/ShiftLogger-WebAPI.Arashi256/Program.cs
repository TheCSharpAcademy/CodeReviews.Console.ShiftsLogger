using ShiftLogger_WebAPI.Arashi256.Models;
using ShiftLogger_WebAPI.Arashi256.Services;

var builder = WebApplication.CreateBuilder(args);
// Add Controllers to the container.
builder.Services.AddControllers();
// Add WorkerService as a transient service (new one each time) to the container.
builder.Services.AddTransient<WorkerService>();
// Add WorkerShiftService as a transient service (new one each time) to the container.
builder.Services.AddTransient<WorkerShiftService>();
// Register DbContext
builder.Services.AddDbContext<ShiftLoggerContext>();
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