using ShiftsLogger.Lonchanick.ContextDataBase;
using ShiftsLogger.Lonchanick.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//me
builder.Services.AddSqlServer<ContextDB>(builder.Configuration.GetConnectionString("ConString"));
builder.Services.AddScoped<IShiftService, ShiftService>();
builder.Services.AddScoped<IWorkerService, WorkerService>();
//me

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//me
//app.UseWelcomePage();

//me

app.MapControllers();

app.Run();
