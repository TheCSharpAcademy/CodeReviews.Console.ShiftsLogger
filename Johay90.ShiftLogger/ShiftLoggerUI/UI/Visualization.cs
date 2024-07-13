using SharedLibrary.DTOs;
using Spectre.Console;

namespace ShiftLoggerUI.UI
{
    internal static class Visualization
    {
        public static void Header()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("Shift Tracker")
                    .Centered()
                    .Color(Color.Aqua));
        }

        public static void DisplayAllEmployees(ICollection<EmployeeDto> employees)
        {
            Header();

            var table = new Table();
            table.Border(TableBorder.Rounded);
            table.Expand();
            table.BorderColor(Color.DarkTurquoise);

            table.AddColumn(new TableColumn("[bold]Id[/]").Centered());
            table.AddColumn(new TableColumn("[bold]Name[/]"));
            table.AddColumn(new TableColumn("[bold]Age[/]").Centered());
            table.AddColumn(new TableColumn("[bold]Phone Number[/]"));
            table.AddColumn(new TableColumn("[bold]Email Address[/]"));

            bool isAlternate = false;
            foreach (var item in employees)
            {
                var rowColor = isAlternate ? "grey" : "white";
                table.AddRow(
                    $"[{rowColor}]{item.Id}[/]",
                    $"[{rowColor}]{item.Name}[/]",
                    $"[{rowColor}]{item.Age}[/]",
                    $"[{rowColor}]{item.PhoneNumber}[/]",
                    $"[{rowColor}]{item.EmailAddress}[/]"
                );
                isAlternate = !isAlternate;
            }

            AnsiConsole.Write(table);
            PromptToContinue();
        }

        public static void DisplayEmployee(EmployeeDto employee)
        {
            Header();

            var table = new Table();
            table.Border(TableBorder.Rounded);
            table.Expand();
            table.BorderColor(Color.DarkTurquoise);

            table.AddColumn(new TableColumn("[bold]Id[/]").Centered());
            table.AddColumn(new TableColumn("[bold]Name[/]"));
            table.AddColumn(new TableColumn("[bold]Age[/]").Centered());
            table.AddColumn(new TableColumn("[bold]Phone Number[/]"));
            table.AddColumn(new TableColumn("[bold]Email Address[/]"));

            table.AddRow(employee.Id.ToString(), employee.Name, employee.Age.ToString(), employee.PhoneNumber, employee.EmailAddress);
            AnsiConsole.Write(table);

            PromptToContinue();
        }

        public static void DisplayAllShifts(ICollection<ShiftDto> shifts)
        {
            Header();

            var table = new Table();
            table.Border(TableBorder.Rounded);
            table.Expand();
            table.BorderColor(Color.DarkTurquoise);

            table.AddColumn(new TableColumn("[bold]Id[/]").Centered());
            table.AddColumn(new TableColumn("[bold]EmployeeId[/]"));
            table.AddColumn(new TableColumn("[bold]StartTime[/]").Centered());
            table.AddColumn(new TableColumn("[bold]EndTime[/]"));
            table.AddColumn(new TableColumn("[bold]Duration[/]"));

            bool isAlternate = false;
            foreach (var item in shifts)
            {
                var rowColor = isAlternate ? "grey" : "white";
                table.AddRow(
                    $"[{rowColor}]{item.Id}[/]",
                    $"[{rowColor}]{item.EmployeeId}[/]",
                    $"[{rowColor}]{item.StartTime}[/]",
                    $"[{rowColor}]{item.EndTime}[/]",
                    $"[{rowColor}]{item.Duration}[/]"
                );
                isAlternate = !isAlternate;
            }

            AnsiConsole.Write(table);
            PromptToContinue();
        }

        public static void DisplayShift(ShiftDto shift)
        {
            Header();

            var table = new Table();
            table.Border(TableBorder.Rounded);
            table.Expand();
            table.BorderColor(Color.DarkTurquoise);

            table.AddColumn(new TableColumn("[bold]Id[/]").Centered());
            table.AddColumn(new TableColumn("[bold]EmployeeId[/]"));
            table.AddColumn(new TableColumn("[bold]StartTime[/]").Centered());
            table.AddColumn(new TableColumn("[bold]EndTime[/]"));
            table.AddColumn(new TableColumn("[bold]Duration[/]"));

            var rowColor = "grey";
            table.AddRow(
                $"[{rowColor}]{shift.Id}[/]",
                $"[{rowColor}]{shift.EmployeeId}[/]",
                $"[{rowColor}]{shift.StartTime}[/]",
                $"[{rowColor}]{shift.EndTime}[/]",
                $"[{rowColor}]{shift.Duration}[/]"
            );

            AnsiConsole.Write(table);
            PromptToContinue();
        }

        public static void Error(string error)
        {
            Header();
            Console.Beep();
            AnsiConsole.Markup("[red]Error occurred: [/]");
            AnsiConsole.WriteLine(error);
            PromptToContinue();
        }

        private static void PromptToContinue()
        {
            AnsiConsole.MarkupLine("\n[italic]Press any key to continue...[/]");
            Console.ReadKey(true);
        }
    }
}