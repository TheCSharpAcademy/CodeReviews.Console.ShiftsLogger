using Microsoft.EntityFrameworkCore;
using ShfitsLogger.yemiodetola.Contexts;
using ShfitsLogger.yemiodetola.Services;

var builder = WebApplication.CreateBuilder(args);
var ConnectionString = builder.Configuration.GetConnectionString("ConnectionString");


builder.Services.AddDbContext<ShiftsContext>(options =>
{
  options.UseSqlServer(ConnectionString);
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IShiftService, ShiftsService>();

var app = builder.Build();

app.MapControllers();

app.Run();
