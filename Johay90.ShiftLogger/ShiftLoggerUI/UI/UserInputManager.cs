using SharedLibrary.DTOs;
using ShiftLoggerUI.Enums;
using Spectre.Console;

namespace ShiftLoggerUI.UI
{
    internal static class UserInputManager
    {
        public static MenuOptions GetMenuOption()
        {
            return AnsiConsole.Prompt(new SelectionPrompt<MenuOptions>()
                .Title("Please choose an [green]API call[/]?")
                .AddChoices(Enum.GetValues<MenuOptions>())
                .PageSize(15));
        }

        public static int GetId() => AnsiConsole.Ask<int>("Type an ID:");

        public static bool Retry()
        {
            return AnsiConsole.Ask<bool>("Would you like to try again <True/False?");
        }

        private static (string name, DateTime dob, string number, string email) CollectEmployeeInfo()
        {
            var name = AnsiConsole.Ask<string>("Name:");
            var date = GetDOB();
            var dob = new DateTime(day: date.Day, month: date.Month, year: date.Year);
            var number = AnsiConsole.Ask<string>("Phone Number:");
            var email = AnsiConsole.Ask<string>("E-Mail:");

            return (name, dob, number, email);
        }

        public static CreateEmployeeDto CreateEmployee()
        {
            var (name, dob, number, email) = CollectEmployeeInfo();

            return new CreateEmployeeDto
            {
                Name = name,
                DateOfBirth = dob,
                PhoneNumber = number,
                EmailAddress = email
            };
        }

        public static UpdateEmployeeDto UpdateEmployee()
        {
            var (name, dob, number, email) = CollectEmployeeInfo();

            return new UpdateEmployeeDto
            {
                Name = name,
                DateOfBirth = dob,
                PhoneNumber = number,
                EmailAddress = email
            };
        }

        private static (DateTime StartTime, DateTime EndTime) CollectShiftInfo()
        {
            DateTime startTime, endTime;
            TimeSpan shiftLength;
            bool isValid;

            do
            {
                startTime = AnsiConsole.Ask<DateTime>("Start Time? (mm-dd-yyyy HH:mm:ss)");
                endTime = AnsiConsole.Ask<DateTime>("End Time? (mm-dd-yyyy HH:mm:ss)");

                shiftLength = endTime - startTime;
                isValid = true;

                if (startTime >= endTime)
                {
                    AnsiConsole.MarkupLine("[red]Error: End time must be after start time.[/]");
                    isValid = false;
                }
                else if (shiftLength.TotalHours <= 1)
                {
                    AnsiConsole.MarkupLine("[red]Error: Shifts must be longer than 1 hour.[/]");
                    isValid = false;
                }
                else if (shiftLength.TotalHours > 16)
                {
                    AnsiConsole.MarkupLine("[red]Error: Shifts cannot last longer than 16 hours.[/]");
                    isValid = false;
                }

                if (!isValid)
                {
                    AnsiConsole.MarkupLine("[yellow]Please enter the shift times again.[/]");
                }

            } while (!isValid);

            return (startTime, endTime);
        }

        public static UpdateShiftDto UpdateShift()
        {
            var (startTime, endTime) = CollectShiftInfo();

            return new UpdateShiftDto
            {
                StartTime = startTime,
                EndTime = endTime
            };
        }

        public static CreateShiftDto CreateShift(int employeeId)
        {
            var (startTime, endTime) = CollectShiftInfo();

            return new CreateShiftDto
            {
                EmployeeId = employeeId,
                StartTime = startTime,
                EndTime = endTime
            };
        }

        private static DateOnly GetDOB()
        {
            DateOnly date;
            do
            {
                date = AnsiConsole.Ask<DateOnly>("Date of Birth? (mm-dd-yyyy)");
            } while (!AnsiConsole.Confirm($"Is {date.Month}-{date.Day}-{date.Year} (mm/dd/yyyy) correct?"));
            return date;
        }
    }
}