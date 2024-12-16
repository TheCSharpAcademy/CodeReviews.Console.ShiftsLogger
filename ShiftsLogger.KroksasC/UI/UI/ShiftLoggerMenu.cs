using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using UI.Services;

namespace UI.UI
{
    internal class ShiftLoggerMenu
    {
        public static async Task Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to shifts logger!");

                var option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What option do you want to choose?")
                        .AddChoices("Create shift", "View shifts", "Delete shift", "Update shift", "Exit")
                );

                switch (option)
                {
                    case "Create shift":
                        await ShiftService.CreateShift();
                        break;
                    case "View shifts":
                        await ShiftService.ViewShifts();
                        break;
                    case "Delete shift":
                        await ShiftService.DeleteShift();
                        break;
                    case "Update shift":
                        await ShiftService.UpdateShift();
                        break;
                    case "Exit":
                        Console.Clear();
                        Console.WriteLine("Goodbye! Shutting down app..");
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
