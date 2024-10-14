using ShiftLoggerUI;

namespace ShiftsLoggerUI;

public class UserInterface
{
    public void Run()
    {
        ShiftMenuHandler shiftMenuHandler = new ShiftMenuHandler();
        shiftMenuHandler.Display();
    }
}