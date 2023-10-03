using ShiftsLoggerConsole.UI;

internal class UserInterface
{
    private ShiftLoggerController _controller;

    public UserInterface(ShiftLoggerController controller)
    {
        _controller = controller;
    }

    public async Task Run()
    {
        List<Shift> shifts = (await _controller.GetShifts()).ToList();

        while (true)
        {
            AnsiConsole.Clear();
            Helpers.RenderTitle("Main Menu");

            foreach (Shift shift in shifts)
            {
                AnsiConsole.WriteLine($"Name : {shift.EmployeeName}");
            }
            Console.ReadLine();
        }
    }
}