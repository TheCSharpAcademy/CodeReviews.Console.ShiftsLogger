using ShiftLoggerConsoleUI.Services;

IShiftLoggerService service = new ShiftLoggerService();
View shiftLoggerView = new(service);
shiftLoggerView.Start();