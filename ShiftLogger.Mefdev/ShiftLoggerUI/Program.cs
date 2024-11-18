using Microsoft.Extensions.DependencyInjection;
using ShiftLogger.Mefdev.ShiftLoggerUI;
using ShiftLogger.Mefdev.ShiftLoggerUI.Inputs;
using ShiftLogger.Mefdev.ShiftLoggerUI.Services;
using ShiftLogger.Mefdev.ShiftLoggerUI.Controllers;

var serviceProvider = new ServiceCollection()
               .AddScoped<ManageShifts>() 
               .AddScoped<UserInput>()
               .AddScoped<WorkerShiftController>()
               .AddScoped<UserInterface>()
               .BuildServiceProvider();

var app = serviceProvider.GetRequiredService<UserInterface>();
await app.MainMenu();