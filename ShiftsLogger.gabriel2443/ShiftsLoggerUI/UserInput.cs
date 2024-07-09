using ShiftsLoggerUI.Helpers;
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
            Console.WriteLine("Press '0' to exit the application");
            Console.WriteLine("Press '1' to view all shifts");
            Console.WriteLine("Press '2' to add a shift");
            Console.WriteLine("Press '3' to delete a shift");
            Console.WriteLine("Press '4' to update a shift");
            Console.WriteLine("----------------------------------");
            var input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    isRunning = false;
                    Environment.Exit(0);
                    break;

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

        shift.FullName = Validate.ReadStringInput("Please enter your name or type '0' to go back to menu", false);
        if (shift.FullName == "0") return;

        shift.StartTime = Validate.ReadDateTimeInput("Please enter the start time in the HH:mm (24 Hour format)");
        shift.EndTime = Validate.ReadDateTimeInput("Please enter the end time in the HH:mm (24 Hour format)");

        while (shift.EndTime <= shift.StartTime)
        {
            Console.WriteLine("End time can't be earlier than or equal to start time. Please try again.");
            shift.EndTime = Validate.ReadDateTimeInput("Please enter the end time in the HH:mm (24 Hour format)");
        }

        shift.Duration = shift.EndTime - shift.StartTime;
        await ShiftHttp.AddShift(shift);
    }

    private async Task ShowShifts()
    {
        Console.Clear();
        var shifts = await ShiftHttp.GetShifts();
        if (shifts.Count == 0)
        {
            Console.WriteLine("No shifts available");
        }
        else
        {
            foreach (var shift in shifts)
            {
                Console.WriteLine($"{shift.Id}. Name-{shift.FullName} \t Start Date:{shift.StartTime.ToString("MM-dd-yyyy HH:mm:ss")} \t End Date:{shift.EndTime.ToString("MM-dd-yyyy HH:mm:ss")} \t Duration {shift.Duration.TotalHours} hours \n");
            }
        }
    }

    private async Task UpdateShift()
    {
        Console.Clear();
        var shifts = await ShiftHttp.GetShifts();
        await ShowShifts();

        var inputId = Validate.ReadShiftIdInput("Please enter the number of the shift you want to update or type 0 to go back to menu");
        if (inputId == 0) return;
        var shiftToUpdate = shifts.FirstOrDefault(s => s.Id == inputId);
        if (shiftToUpdate == null)
        {
            Console.WriteLine("Shift does not exist");
            return;
        }

        shiftToUpdate.FullName = Validate.ReadStringInput("Please enter your name or type '0' to go back to menu", false);
        if (shiftToUpdate.FullName == "0") return;

        shiftToUpdate.StartTime = Validate.ReadDateTimeInput("Please enter the start time in the HH:mm (24 Hour format)");
        shiftToUpdate.EndTime = Validate.ReadDateTimeInput("Please enter the end time in the HH:mm (24 Hour format)");

        while (shiftToUpdate.EndTime <= shiftToUpdate.StartTime)
        {
            Console.WriteLine("End time can't be earlier than or equal to start time. Please try again.");
            shiftToUpdate.EndTime = Validate.ReadDateTimeInput("Please enter the end time in the HH:mm (24 Hour format)");
        }

        shiftToUpdate.Duration = shiftToUpdate.EndTime - shiftToUpdate.StartTime;
        await ShiftHttp.UpdateShift(shiftToUpdate);
    }

    private async Task DeleteShift()
    {
        Console.Clear();
        var shifts = await ShiftHttp.GetShifts();
        await ShowShifts();

        var inputId = Validate.ReadShiftIdInput("Please enter the number of the shift you want to delete or type 0 to go back to menu");
        if (inputId == 0) return;
        var shiftToDelete = shifts.FirstOrDefault(s => s.Id == inputId);
        if (shiftToDelete == null)
        {
            Console.WriteLine("Shift does not exist");
            return;
        }

        await ShiftHttp.DeleteShift(inputId);
    }
}