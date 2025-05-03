using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Ryanw84.Data;

using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<ShiftsDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

if(app.Environment.IsDevelopment())
    {
   app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseAuthentication();
    app.UseAuthorization();

    using var scope = app.Services.CreateScope();

    var context = scope.ServiceProvider.GetRequiredService<ShiftsDbContext>();

    }

app.MapControllers();

app.Run();
