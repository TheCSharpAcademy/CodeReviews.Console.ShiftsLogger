using ShiftLoggerUi.DTOs;
using ShiftLoggerUi.Services;
using Spectre.Console;
using System.Globalization;

namespace ShiftLoggerUi
{
    internal class UserInput
    {
        public static DateTime GetDateTimeInput(string prompt)
        {
            string dateTimeString = AnsiConsole.Prompt(
                new TextPrompt<string>(prompt)
                    .Validate(input =>
                    {
                        if (!DateTime.TryParseExact(input, "yyyy-MM-dd HH:mm",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                        {
                            return ValidationResult.Error("Invalid format. Use YYYY-MM-DD HH:mm (e.g., 2025-02-24 14:30).");
                        }

                        return ValidationResult.Success();
                    })
            );

            return DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
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
                Utilities.DisplayMessage("No workers available. Cannot proceed.", "red");
                Console.ReadKey();
                return null;
            }

            workers = workers.OrderBy(w => w.WorkerId).ToList();

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
                Utilities.DisplayMessage("No shifts available for this worker. Cannot proceed.", "red");
                Console.ReadKey();
                return null;
            }

            return AnsiConsole.Prompt(
                new SelectionPrompt<ShiftDto>()
                    .Title("Select a shift:")
                    .AddChoices(shifts)
                    .UseConverter(s => $"{s.ShiftId}: {s.StartDate} - {s.EndDate}"));
        }

        public static DepartmentDto GetDepartmentOptionInput()
        {
            var departmentService = new DepartmentService();
            List<DepartmentDto> departments = departmentService.GetAllDepartments();

            if (departments.Count == 0)
            {
                Utilities.DisplayMessage("No departments available. Cannot proceed.", "red");
                Console.ReadKey();
                return null;
            }

            departments = departments.OrderBy(d => d.DepartmentId).ToList();

            return AnsiConsole.Prompt(
                new SelectionPrompt<DepartmentDto>()
                    .Title("Select a department:")
                    .AddChoices(departments)
                    .UseConverter(d => $"{d.DepartmentId}: {d.DepartmentName}"));
        }

    }
}