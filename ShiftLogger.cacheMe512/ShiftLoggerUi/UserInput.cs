using ShiftLoggerUi.DTOs;
using ShiftLoggerUi.Services;
using Spectre.Console;

namespace ShiftLoggerUi
{
    internal class UserInput
    {
        public static DateTime GetDateTimeInput(string prompt)
        {
            return AnsiConsole.Prompt(
                new TextPrompt<DateTime>(prompt)
                    .Validate(input => input > DateTime.MinValue ? ValidationResult.Success() : ValidationResult.Error("Invalid date/time.")));
        }

        public static int GetIntInput(string prompt)
        {
            return AnsiConsole.Prompt(
                new TextPrompt<int>(prompt)
                    .Validate(input => input > 0 ? ValidationResult.Success() : ValidationResult.Error("Value must be greater than zero."))
            );
        }

        public static string GetStringInput(string prompt)
        {
            return AnsiConsole.Prompt(
                new TextPrompt<string>(prompt)
                    .Validate(input => !string.IsNullOrWhiteSpace(input) ? ValidationResult.Success() : ValidationResult.Error("Input cannot be empty."))
            );
        }

        public static DateTime GetDateInput(string prompt)
        {
            return AnsiConsole.Prompt(
                new TextPrompt<DateTime>(prompt)
                    .Validate(input => input > DateTime.MinValue ? ValidationResult.Success() : ValidationResult.Error("Invalid date."))
            );
        }

        public static bool GetConfirmation(string prompt)
        {
            return AnsiConsole.Confirm(prompt);
        }

        public static WorkerDto GetWorkerOptionInput()
        {
            var workerService = new WorkerService();
            List<WorkerDto> workers = workerService.GetAllWorkers();

            if (workers.Count == 0)
            {
                Console.WriteLine("No workers available. Cannot proceed.");
                Console.ReadKey();
                return null;
            }

            return AnsiConsole.Prompt(
                new SelectionPrompt<WorkerDto>()
                    .Title("Select a worker:")
                    .AddChoices(workers)
                    .UseConverter(w => $"{w.WorkerId}: {w.FirstName} {w.LastName}"));
        }

        public static ShiftDto GetShiftOptionInput(int workerId)
        {
            var shiftService = new ShiftService();
            List<ShiftDto> shifts = shiftService.GetShiftsByWorker(workerId);

            if (shifts.Count == 0)
            {
                Console.WriteLine("No shifts available for this worker. Cannot proceed.");
                Console.ReadKey();
                return null;
            }

            return AnsiConsole.Prompt(
                new SelectionPrompt<ShiftDto>()
                    .Title("Select a shift:")
                    .AddChoices(shifts)
                    .UseConverter(s => $"{s.ShiftId}: {s.StartDate} - {s.EndDate}"));
        }
    }
}