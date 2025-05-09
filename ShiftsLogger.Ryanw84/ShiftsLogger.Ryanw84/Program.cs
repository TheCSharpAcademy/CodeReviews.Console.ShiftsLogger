using Microsoft.EntityFrameworkCore;

using Scalar.AspNetCore;

using ShiftsLogger.Ryanw84.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<ShiftsDbContext>(options =>	
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //app.UseAuthentication();
    //app.UseAuthorization();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Shifts Logger")
            .WithTheme(ScalarTheme.Saturn)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
            .WithModels(true)
            .WithLayout(ScalarLayout.Modern);
    });
}
app.MapControllers();
app.Run();
