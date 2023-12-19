using ShiftsLoggerUI;
var menu = new Menu();
try
{
    menu.Display();
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
}
