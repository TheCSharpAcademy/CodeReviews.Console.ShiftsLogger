using Microsoft.EntityFrameworkCore;

using Scalar.AspNetCore;

using ShiftsLogger.Ryanw84.Data;
using ShiftsLogger.Ryanw84.Mapping;
using ShiftsLogger.Ryanw84.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IShiftService , ShiftService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddOpenApi();

builder
	.Services.AddControllers()
	.AddJsonOptions(options =>
		options.JsonSerializerOptions.ReferenceHandler = System
			.Text
			.Json
			.Serialization
			.ReferenceHandler
			.IgnoreCycles
	);

builder.Services.AddDbContext<ShiftsDbContext>(opt =>
	opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
	options
		.WithTitle("Shifts Logger API")
		.WithTheme(ScalarTheme.Saturn)
		.WithDefaultHttpClient(ScalarTarget.CSharp , ScalarClient.HttpClient)
		.WithModels(true)
		.WithLayout(ScalarLayout.Modern);
});
app.UseAuthentication();
app.UseAuthorization();

using var scope = app.Services.CreateScope();
