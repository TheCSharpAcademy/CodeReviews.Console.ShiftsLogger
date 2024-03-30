using ShiftsLoggerAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<IShiftService>(new SqlServerShiftService(new ShiftsContext(builder.Configuration)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/shifts", ShiftController.GetShiftsAsync);

app.MapGet("/shifts/{id}", ShiftController.GetShiftByIdAsync);

app.MapGet("/shifts/employee/{id}", ShiftController.GetShiftsByEmployeeIdAsync);

app.MapGet("/shifts/employee/{id}/running", ShiftController.GetRunningShiftsByEmployeeIdAsync);

app.MapPost("/shifts/start", ShiftController.StartShiftAsync).AddEndpointFilter(Validate.StartShiftIsValid);

app.MapPost("/shifts/end", ShiftController.EndShiftAsync).AddEndpointFilter(Validate.EndShiftIsValid);

app.MapPut("/shifts/update", ShiftController.UpdateShiftAsync).AddEndpointFilter(Validate.UpdatedShiftIsValid);

app.MapDelete("/shifts/{id}", ShiftController.DeleteShiftAsync);

app.Run();
