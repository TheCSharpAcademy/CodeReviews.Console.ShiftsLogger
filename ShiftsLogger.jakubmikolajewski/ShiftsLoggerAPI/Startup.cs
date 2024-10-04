using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Data;

var builder = WebApplication.CreateBuilder(args);

var contextOptions = new DbContextOptionsBuilder<ShiftsLoggerContext>()
    .UseSqlServer(builder.Configuration.GetConnectionString("ShiftsLoggerDatabase"))
    .Options;

using (var context = new ShiftsLoggerContext(contextOptions))
{
    context.Database.Migrate(); 
}

builder.Services.AddControllers();

builder.Services.AddDbContext<ShiftsLoggerContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ShiftsLoggerDatabase")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
