using Microsoft.EntityFrameworkCore;
using ShiftsLoggerApi.Controllers;
using ShiftsLoggerApi.Factory;

var app = HostFactory.CreateDbHost(args);

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
context.Database.Migrate();

app.Run();
