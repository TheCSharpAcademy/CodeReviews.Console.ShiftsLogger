using Buutyful.ShiftsLogger.Api.Data;
using Buutyful.ShiftsLogger.Domain.Contracts.WorkerContracts;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddScoped<WorkerDataAccess>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/worker", async (WorkerDataAccess data) =>
{
    var workers = await data.GetAsync();
    return Results.Ok(workers);
});
app.MapGet("/worker/{Id}", async (WorkerDataAccess data, Guid id) =>
{
    var worker = await data.GetByIdAsync(id);
    return worker is null ? Results.NotFound() : Results.Ok(worker);
});
app.MapPost("/worker", async (WorkerDataAccess data, CreateWorkerRequest workerRequest) =>
{
    var worker = await data.AddAsync(workerRequest);
    return Results.Created("/worker/{Id}", worker);
});
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
            title = "Internal Server Error",
            detail = exception?.Message
        };
        var json = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
    });
});


app.UseHttpsRedirection();


app.Run();

