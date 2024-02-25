using Buutyful.ShiftsLogger.Api.Data;
using Buutyful.ShiftsLogger.Api.EndPoints.Shift;
using Buutyful.ShiftsLogger.Api.EndPoints.Worker;
using Buutyful.ShiftsLogger.Domain.Contracts.WorkerContracts;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddScoped<WorkerDataAccess>();
builder.Services.AddScoped<ShiftDataAccess>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapWorkerEndPoints();
app.MapShiftEndPoints();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        var response = new
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Type = "Server Error",
            Title = "Internal Server Error",
            Detail = exception?.Message
        };
        var json = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
    });
});


app.UseHttpsRedirection();


app.Run();

