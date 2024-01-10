using ShiftsLoggerUI;

ShiftsLoggerService shiftsLoggerService = new ShiftsLoggerService();
MenuBuilders menuBuilder = new MenuBuilders(shiftsLoggerService);

await menuBuilder.MainMenu();

Console.ReadLine();