using ConsoleFrontEnd.MenuSystem;
using ConsoleFrontEnd.Models;
using ConsoleFrontEnd.Models.Dtos;
using ConsoleFrontEnd.Models.FilterOptions;
using ConsoleFrontEnd.Services;
using Spectre.Console;

namespace ConsoleFrontEnd.Controller
{
    public class ShiftController()
    {
        // This class acts as a controller for managing shifts, handling user input and interaction with the ShiftService.
        // It provides methods to create a shift and retrieve all shifts with optional filtering.
        public readonly MenuSystem.UserInterface userInterface = new();
        internal readonly ShiftService shiftService = new ShiftService();

        internal ShiftFilterOptions shiftFilterOptions = new()
        {
            WorkerId = null,
            LocationId = null,
            StartTime = null,
            EndTime = null,
        };

        // Helpers
        public async Task (ApiResponseDto<List<Shifts>>checkedShift) NotFoundHelper(ApiResponseDto<List<Shifts>>? shift , int shiftId)
        {
            var checkedShift = await shiftService.GetShiftById(shiftId);
            while (checkedShift.ResponseCode is System.Net.HttpStatusCode.NotFound)
            {
                var exitSelection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Try again or exit?")
                        .AddChoices(new[] { "Try Again" , "Exit" })
                );
                if (exitSelection is "Exit")
                {
                    Console.Clear();
                    return;
                }
                else if (exitSelection is "Try Again")
                {
                    Console.Clear();
                    shiftId = userInterface.GetShiftByIdUi();
                    checkedShift = await shiftService.GetShiftById(shiftId); //TODO: resolve this error
                }
                return;
            }
        }


        // CRUD
        public async Task CreateShift()
        {
            try
            {
                var shift = userInterface.CreateShiftUi();
                var createdShift = await shiftService.CreateShift(shift);
                if (
                    createdShift.ResponseCode is not System.Net.HttpStatusCode.Created
                    || createdShift.ResponseCode is not System.Net.HttpStatusCode.OK
                )
                {
                    AnsiConsole.MarkupLine(
                        $"[red]Error: Failed to create shift: {createdShift.ResponseCode}[/]"
                    );
                }
                else
                {
                    AnsiConsole.MarkupLine("[green]Shift created successfully![/]");
                    AnsiConsole.MarkupLine($"[green]Shift ID: {createdShift.Data.ShiftId}[/]");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Exception: {ex.Message}[/]");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }

        public async Task GetAllShifts()
        {
            try
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new Rule("[bold yellow]View All Shifts[/]").RuleStyle("yellow").Centered()
                );

                var filterOptions = userInterface.FilterShiftsUi();

                shiftFilterOptions = filterOptions;
                var shifts = await shiftService.GetAllShifts(shiftFilterOptions);
                userInterface.DisplayShiftsTable(shifts.Data);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Exception: {ex.Message}[/]");
            }
        }

        public async Task GetShiftById()
        {
            try
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new Rule("[bold yellow]View Shift by ID[/]").RuleStyle("yellow").Centered()
                );
                var shiftId = userInterface.GetShiftByIdUi();
                var shift = await shiftService.GetShiftById(shiftId);
                while (shift.ResponseCode is System.Net.HttpStatusCode.NotFound)
                {
                    var exitSelection = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Try again or exit?")
                            .AddChoices(new[] { "Try Again", "Exit" })
                    );
                    if (exitSelection is "Exit")
                    {
                        Console.Clear();
                        return;
                    }
                    else if (exitSelection is "Try Again")
                    {
                        Console.Clear();
                        shiftId = userInterface.GetShiftByIdUi();
                        shift = await shiftService.GetShiftById(shiftId);
                    }
                    userInterface.DisplayShiftsTable(shift.Data);
                    userInterface.ContinueAndClearScreen();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }

        public async Task UpdateShift()
        {
            try
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new Rule("[bold yellow]Update Shift[/]").RuleStyle("yellow").Centered()
                );
                var shiftId = userInterface.GetShiftByIdUi();
                var existingShift = await shiftService.GetShiftById(shiftId);

                while (existingShift.ResponseCode is System.Net.HttpStatusCode.NotFound)
                {
                    var exitSelection = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Try again or exit?")
                            .AddChoices(new[] { "Try Again", "Exit" })
                    );
                    if (exitSelection is "Exit")
                    {
                        Console.Clear();
                        return;
                    }
                    else if (exitSelection is "Try Again")
                    {
                        Console.Clear();
                        shiftId = userInterface.GetShiftByIdUi();
                        existingShift = await shiftService.GetShiftById(shiftId);
                    }

                    var updatedShift = userInterface.UpdateShiftUi(existingShift.Data);

                    var updatedShiftResponse = await shiftService.UpdateShift(
                        shiftId,
                        updatedShift
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception - {ex.Message}");
            }
        }

        public async Task DeleteShift()
        {
            try
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new Rule("[bold yellow]Delete Shift[/]").RuleStyle("yellow").Centered()
                );
                var shiftId = userInterface.GetShiftByIdUi();
                var deletedShift = await shiftService.DeleteShift(shiftId);

                while (deletedShift.ResponseCode is System.Net.HttpStatusCode.NotFound)
                {
                    var exitSelection = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("\nTry again or exit?")
                            .AddChoices(new[] { "Try Again", "Exit" })
                    );
                    if (exitSelection is "Exit")
                    {
                        Console.Clear();
                        return;
                    }
                    else if (exitSelection is "Try Again")
                    {
                        Console.Clear();
                        shiftId = userInterface.GetShiftByIdUi();
                        deletedShift = await shiftService.DeleteShift(shiftId);
                    }
                }
                userInterface.ContinueAndClearScreen();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Try Pass failed in Shift Controller: Delete Shift {ex}");
                userInterface.ContinueAndClearScreen();
            }
        }

       
    }
}
