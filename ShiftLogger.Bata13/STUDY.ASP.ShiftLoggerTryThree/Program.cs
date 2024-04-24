using STUDY.ASP.ShiftLoggerTryThree.Data;
using STUDY.ASP.ShiftLoggerTryThree.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IShiftLoggerService, ShiftLoggerService>();
builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();