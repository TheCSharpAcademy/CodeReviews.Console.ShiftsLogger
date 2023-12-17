using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Models;
using ShiftsLoggerUI;

var options = new DbContextOptionsBuilder<ShiftContext>()
            .UseSqlServer("SqlServerConnectionString")
            .Options;

// Create the ShiftContext (database context)
ShiftContext shiftContext = new ShiftContext(options);

// Create the ShiftsLoggerService and MenuBuilders with dependencies
ShiftsLoggerService shiftsLoggerService = new ShiftsLoggerService(shiftContext);
MenuBuilders menuBuilder = new MenuBuilders(shiftsLoggerService);

// Call the MainMenu method to start the application
menuBuilder.MainMenu();

Console.ReadLine();