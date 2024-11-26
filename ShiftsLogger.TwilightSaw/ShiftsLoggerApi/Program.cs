using Microsoft.EntityFrameworkCore;
using ShiftsLoggerApi.Controllers;
using ShiftsLoggerApi.Factory;

var app = HostFactory.CreateWebApplication(args);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
context.Database.Migrate();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
