using ShiftLoggerUI.UI;
using ShiftLoggerUI.Models;
using Spectre.Console;

namespace ShiftLoggerUI.Services;

public class ShiftLoggerService
{

    private readonly HttpClient _httpClient;
    private readonly ApiService _apiService;
    private readonly ValidationService _validationService;
    private readonly UserInput _userInput;

    // Constructor hvor du kan initialisere felterne
    public ShiftLoggerService()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7043") };
        _apiService = new ApiService(_httpClient);
        _validationService = new ValidationService();
        _userInput = new UserInput(_validationService);
    }

    public async Task Run()
    {
        while (true)
        {
            Console.Clear();
            var menuOptions = new List<MenuOption>
            {
                new MenuOption("Start Shift", async () => await StartShift()),
                new MenuOption("End Shift", async () => await EndShift()),
                new MenuOption("View Shift", async () => await ViewShifts()),
                new MenuOption("Exit", () =>
                {
                    Environment.Exit(0);
                    return Task.CompletedTask; // Brug Task.CompletedTask for at matche Task-return typen
                })
            };

            var selectedOption = AnsiConsole.Prompt(new SelectionPrompt<MenuOption>()
                .Title("Shift Logger")
                .PageSize(10)
                .AddChoices(menuOptions)
                .UseConverter(m => m.Name));

            await selectedOption.Action();

            if (selectedOption.Name == "Exit")
            {
                break;
            }
        }
    }

    public async Task StartShift()
    {
        var startShift = _userInput.GetStartShiftDetails();
        var startResponse = await _apiService.StartShiftAsync(startShift);
        Console.Clear();

        if (startResponse.IsSuccessStatusCode)
        {
            Console.WriteLine("Shift started successfully.");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Failed to start shift.");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }


    public async Task EndShift()
    {
        var endResponse = await _apiService.EndShiftAsync();
        Console.Clear();

        if (endResponse.IsSuccessStatusCode)
        {
            Console.WriteLine("Shift ended successfully.");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Failed to end shift.");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }

    public async Task ViewShifts()
    {

        var shifts = await _apiService.GetShiftsAsync();

        if (shifts == null)
        {
            Console.WriteLine("Failed to retrieve shift.");
            Console.ReadKey();
            return;
        }

        Console.Clear();

        foreach (var shift in shifts)
        {
            Console.WriteLine($"Employee Name: {shift.EmployeeName}");
            Console.WriteLine($"Start: {shift.Start}");
            Console.WriteLine($"End: {shift.End}");
            Console.WriteLine($"Duration: {shift.Duration}");
            Console.WriteLine();
        }
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }
}