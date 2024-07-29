using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Dejmenek.API.Data;
using ShiftsLogger.Dejmenek.API.Data.Interfaces;
using ShiftsLogger.Dejmenek.API.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ShiftsContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ShiftsDb"));
});
builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddScoped<IShiftsRepository, ShiftsRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});
builder.Services.AddControllersWithViews(options => options.SuppressAsyncSuffixInActionNames = false);

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
