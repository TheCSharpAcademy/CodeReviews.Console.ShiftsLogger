using ShiftLoggerConsoleUI.Models;

namespace ShiftLoggerConsoleUI.Services;

internal class View
{
    private readonly IShiftLoggerService _shiftService;

    public View(IShiftLoggerService shiftService)
    {
        _shiftService = shiftService;
    }

    public bool Exit { get; set; } = false;

    public void Start()
    {

        while (Exit == false)
        {
            Console.Clear();

            Console.WriteLine("Shift Logger");
            Console.WriteLine("_____________");
            Console.WriteLine("V) View All Shifts");
            Console.WriteLine("G) View Single Shift");
            Console.WriteLine("A) Add Shift");
            Console.WriteLine("U) Update Shift");
            Console.WriteLine("D) Delete Shift");
            Console.WriteLine("0) Exit");
            Console.WriteLine("____________");
            Console.Write("Select an option: ");
            string option = Console.ReadLine();
            MenuOperator(option).Wait();
        }
    }

    private async Task MenuOperator(string option)
    {
        switch (option.ToLower())
        {
            case "v":
                await ShowAllShifts();
                break;
            case "g":
                await ShowShift();
                break;
            case "a":
                await AddNewShift();
                break;
            case "u":
                await UpdateShift();
                break;
            case "d":
                await DeleteShift();
                break;
            case "0":
                Exit = true;
                return;
            default:
                Console.WriteLine("Inavlid option");
                break;
        }
    }

    private async Task ShowAllShifts()
    {
        var shifts = await _shiftService.GetShifts();
        Console.WriteLine("\n\n_______________________________________________Shifts_________________________________________________________");
        foreach (var shift in shifts)
        {
            Console.WriteLine($"Shift Id: {shift.Id} Shift start: {shift.ShiftStart}, Shift end: {shift.ShiftEnd}, Shift duration: {shift.Duration}");
            Console.WriteLine("_________________________________________________________________________________________________________");
        }
        Console.WriteLine();
        Console.Write("Press any key to continue: ");
        Console.ReadLine();
    }

    private async Task ShowShift()
    {
        await ShowAllShifts();
        int id = UserInput.GetIdInput();
        DisplayShiftDto shift = await _shiftService.GetShiftsById(id);
        if (shift != null)
        {
            Console.WriteLine($"_____Shift__________");
            Console.WriteLine($"Shift Id: {shift.Id}");
            Console.WriteLine($"Shift start: {shift.ShiftStart}");
            Console.WriteLine($"Shift end: {shift.ShiftEnd}");
            Console.WriteLine($"Shift duration: {shift.Duration}");
            Console.WriteLine("______________________");

            Console.Write("Press any key to continue: ");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Error getting shify");
        }
    }

    private async Task AddNewShift()
    {
        DateTime shiftStart = UserInput.GetShiftStartInput();
        DateTime shiftEnd = UserInput.GetEndDateInput(shiftStart);
        string response = await _shiftService.AddShift(shiftStart, shiftEnd);
        await Console.Out.WriteLineAsync(response);
        Console.Write("Press any key to continue: ");
        Console.ReadLine();
    }

    private async Task UpdateShift()
    {
        await ShowAllShifts();
        int id = UserInput.GetIdInput();
        DateTime updatedShiftStart = UserInput.GetShiftStartInput();
        DateTime updatedShiftEnd = UserInput.GetEndDateInput(updatedShiftStart);
        var response = await _shiftService.UpdateShift(id, updatedShiftStart, updatedShiftEnd);
        await Console.Out.WriteLineAsync(response);
        Console.Write("Press any key to continue: ");
        Console.ReadLine();
    }

    private async Task DeleteShift()
    {
        await ShowAllShifts();
        int id = UserInput.GetIdInput();
        var response = await _shiftService.DeleteShift(id);
        await Console.Out.WriteLineAsync(response);
        Console.Write("Press any key to continue: ");
        Console.ReadLine();
    }

}