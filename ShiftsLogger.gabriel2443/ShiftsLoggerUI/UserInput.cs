using ShiftsLoggerUI.Models;

namespace ShiftsLoggerUI;

public class UserInput
{
    public async Task Menu()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("Shift menu\n");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Press '1' to view all shifts");
            Console.WriteLine("Press '2' to add a shift");
            Console.WriteLine("Press '3' to delete a shift");
            Console.WriteLine("Press '4' to update a shift");
            Console.WriteLine("----------------------------------");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await ShowShifts();
                    break;

                case "2":
                    await AddShift();
                    break;

                case "3":
                    await DeleteShift();
                    break;

                case "4":
                    await UpdateShift();
                    break;

                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
    }

    private async Task AddShift()
    {
        Console.Clear();
        var shift = new Shift();

        Console.WriteLine("Please enter your name or type 0  to go back to menu");
        var name = Console.ReadLine();
        if (name == "0") return;
        while (string.IsNullOrEmpty(name)) Console.WriteLine("Input can not be empty try again.");

        Console.WriteLine("Please enter the end date in the HH:mm format");
        var startDate = Console.ReadLine();
        while (!DateTime.TryParseExact(startDate, "HH:mm", null, System.Globalization.DateTimeStyles.None, out _) || string.IsNullOrEmpty(startDate))
        {
            Console.WriteLine("Invalid date time");
            startDate = Console.ReadLine();
        }

        Console.WriteLine("Please enter the end date in the HH:mm format");
        var endDate = Console.ReadLine();

        while (DateTime.Parse(endDate) <= DateTime.Parse(startDate))
        {
            Console.WriteLine("End time can't be lower than start time");
            endDate = Console.ReadLine();
        }
        while (!DateTime.TryParseExact(endDate, "HH:mm", null, System.Globalization.DateTimeStyles.None, out _) || string.IsNullOrEmpty(startDate))
        {
            Console.WriteLine("Invalid date time");
            endDate = Console.ReadLine();
        }

        shift.FullName = name;
        shift.StartTime = DateTime.Parse(startDate);
        shift.EndTime = DateTime.Parse(endDate);
        shift.Duration = DateTime.Parse(startDate) - DateTime.Parse(endDate);
        await ShiftHttp.AddShift(shift);
    }

    private async Task ShowShifts()
    {
        Console.Clear();
        var shifts = await ShiftHttp.GetShifts();
        if (shifts.Count == 0) Console.WriteLine("No shifts available");
        foreach (var shift in shifts)
        {
            Console.WriteLine($"{shift.Id}. Name-{shift.FullName} \t Start Date:{shift.StartTime.ToString("MM-dd-yyyy HH:mm:ss")} \t End Date:{shift.EndTime.ToString("MM-dd-yyyy HH:mm:ss")} \t Duration {shift.Duration.TotalHours} hours \n");
        }
    }

    private async Task UpdateShift()
    {
        Console.Clear();
        var shifts = await ShiftHttp.GetShifts();
        await ShowShifts();
        if (shifts.Count == 0)
        {
            Console.WriteLine("No shifts available");
            return;
        }
        Console.WriteLine("Please enter the number of shift you want to update");
        var shiftId = Console.ReadLine();

        while (!int.TryParse(shiftId, out _))
        {
            Console.WriteLine("Invalid shift ID. Please enter a valid number.");
            shiftId = Console.ReadLine();
        }

        var shift = new Shift();

        Console.WriteLine("Please enter your name or type '0' to go back to menu ");
        var name = Console.ReadLine();
        if (name == "0") return;
        while (string.IsNullOrEmpty(name)) Console.WriteLine("Input can not be empty try again.");

        Console.WriteLine("Please enter the start date in the HH:mm format");
        var startDate = Console.ReadLine();
        while (!DateTime.TryParseExact(startDate, "HH:mm", null, System.Globalization.DateTimeStyles.None, out _) || string.IsNullOrEmpty(startDate))
        {
            Console.WriteLine("Invalid date time");
            startDate = Console.ReadLine();
        };

        Console.WriteLine("Please enter the end date in the HH:mm format");
        var endDate = Console.ReadLine();

        while (DateTime.Parse(endDate) <= DateTime.Parse(startDate))
        {
            Console.WriteLine("End time can't be lower than start time");
            endDate = Console.ReadLine();
        }
        while (!DateTime.TryParseExact(endDate, "HH:mm", null, System.Globalization.DateTimeStyles.None, out _) || string.IsNullOrEmpty(startDate))
        {
            Console.WriteLine("Invalid date time");
            endDate = Console.ReadLine();
        };

        shift.Id = Convert.ToInt32(shiftId);
        shift.FullName = name;
        shift.StartTime = DateTime.Parse(startDate);
        shift.EndTime = DateTime.Parse(endDate);
        shift.Duration = DateTime.Parse(endDate) - DateTime.Parse(startDate);
        await ShiftHttp.UpdateShift(shift);
    }

    private async Task DeleteShift()
    {
        Console.Clear();

        await ShowShifts();
        Console.WriteLine("Please insert the number you want to delete");
        var input = Console.ReadLine();
        while (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Input can not be empty please choose a number");
            input = Console.ReadLine();
        }
        await ShiftHttp.DeleteShift(Convert.ToInt32(input));
    }
}