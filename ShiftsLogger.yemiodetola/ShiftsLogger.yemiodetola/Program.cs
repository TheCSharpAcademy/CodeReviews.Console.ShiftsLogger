using Microsoft.EntityFrameworkCore;
using ShfitsLogger.yemiodetola.Contexts;

var builder = WebApplication.CreateBuilder(args);
var ConnectionString = builder.Configuration.GetConnectionString("ConnectionString");


builder.Services.AddDbContext<ShiftsContext>(options =>
{
  options.UseSqlServer(ConnectionString);
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
