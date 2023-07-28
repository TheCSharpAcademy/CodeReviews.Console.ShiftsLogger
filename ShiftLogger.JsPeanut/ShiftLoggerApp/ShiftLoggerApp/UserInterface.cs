using Spectre.Console;

namespace ShiftLoggerApp
{
    public class UserInterface
    {
        public static void ShowShift(Shift shift)
        {
            var panel = new Panel($@"Id: {shift.Id}
Full name: {shift.FullName}
Start time: {shift.StartTime}
End time: {shift.EndTime}
Worked time: {shift.WorkedTime}");
            panel.Header = new PanelHeader("Shift info:");
            panel.Padding = new Padding(2, 2, 2, 2);

            AnsiConsole.Write(panel);

            Console.WriteLine("Enter any key to continue");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
