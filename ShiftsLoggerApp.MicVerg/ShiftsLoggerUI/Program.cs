using ShiftsLoggerUI;

ShiftsLoggerService shiftsLoggerService = new ShiftsLoggerService();
MenuBuilders menuBuilder = new MenuBuilders(shiftsLoggerService);

menuBuilder.MainMenu();

Console.ReadLine();