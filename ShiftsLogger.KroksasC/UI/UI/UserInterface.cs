using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Models;
using UI.Helpers;
using Spectre.Console;
using UI.Services;
using UI.Controllers;
using System.Transactions;

namespace UI.UI
{
    internal class UserInterface
    {
        public static async Task<int> GetDeleteInput()
        {
            var shift = await GetShiftById("delete");
            return shift.Id;
        }
        public static async Task<Shift> GetUpdateInput()
        {
            var shift = await GetShiftById("update");

            var options = new[] { "Update Name", "Update Start Time", "Update End Time" };

            // Display a checklist for multiple selection
            var selectedOptions = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .Title("Choose your [green]options[/]:")
                    .PageSize(10) // Number of items to display at once
                    .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                    .InstructionsText("[grey](Press [blue]<space>[/] to toggle an option, [green]<enter>[/] to accept)[/]")
                    .AddChoices(options)
            );

            if (selectedOptions.Contains("Update Name"))
            {
                Console.WriteLine("Enter new name:");
                string? name = Console.ReadLine();
                shift.Name = Validation.ValidateName(name);
            }

            if(selectedOptions.Contains("Update Start Time"))
            {
                Console.WriteLine("Enter new start time for shift(format:(yyyy-MM-dd HH:mm:ss))");
                string? start = Console.ReadLine();
                shift.StartTime = Validation.ValidateDate(start);
            }

            if (selectedOptions.Contains("Update End Time"))
            {
                Console.WriteLine("Enter new end time for shift(format:(yyyy-MM-dd HH:mm:ss))");
                string? end = Console.ReadLine();
                shift.EndTime = Validation.ValidateDate(end);
            }

            if(selectedOptions.Count == 0)
            {
                Console.WriteLine("No options were choosed. Press any key to return");
                Console.ReadLine();
            }
            return shift;

        }
        public static Shift GetCreateInput()
        {
            Console.WriteLine("What is the name of the shift?");
            string? name = Console.ReadLine();
            name = Validation.ValidateName(name);

            Console.WriteLine("What is the start time of the shift?(format:(yyyy-MM-dd HH:mm:ss))");
            string? start = Console.ReadLine();
            DateTime startTime = Validation.ValidateDate(start);

            Console.WriteLine("What is the end time of the shift?(format:(yyyy-MM-dd HH:mm:ss))");
            string? end = Console.ReadLine();
            DateTime endTime = Validation.ValidateDate(end);

            return new Shift { EndTime = endTime, StartTime = startTime, Name = name };
        }
        public static async Task ShowShifts(List<Shift> shifts)
        {
            Table table = new Table();
            table.AddColumns("Id", "Name", "Start shift", "End shift", "Duration");
            foreach (Shift shift in shifts) 
            {
                table.AddRow(shift.Id.ToString(), shift.Name, shift.StartTime.ToString(), shift.EndTime.ToString(), shift.Duration.ToString());
            }
            AnsiConsole.Render(table);
            Console.WriteLine("Enter any key to continue");
            Console.ReadLine();
        }
        public static async Task<Shift> GetShiftById(string action)
        {
            await ShiftService.ViewShifts();

            List<Shift> shifts = await ShiftController.GetShifts();

            var option = AnsiConsole.Ask<string?>($"Enter shift id that you want to choose to {action}");

            int choosedId = Validation.ValidateNumber(option);

            if (!shifts.Any(s => s.Id == choosedId))
            {
                Console.WriteLine("Id that you choose doesn't exist! Press any key to return");
                Console.ReadLine();

                await ShiftLoggerMenu.Run();

                return null;
            }
            else
            {
                return shifts.First(s => s.Id == choosedId);
            }
        }
    }
}
