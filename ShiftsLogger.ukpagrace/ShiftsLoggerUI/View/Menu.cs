using Spectre.Console;
using ShiftsLoggerUI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShiftsLoggerUI.Utilities;

namespace ShiftsLoggerUI.View
{
    internal class Menu
    {


        public static string GetOption()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("What Shift Operation [green]do you want to perform[/]?")
                .AddChoices(new[] {
                    "Create", "Update", "Delete",
                    "GetOne", "GetAll", "Exit",
                }));
        }

        public static async Task PerformOperation()
        {
            ApiService apiService = new();
            
            var option = GetOption();

            switch (option)
            {
                case "Create":
                    await apiService.PostShiftLog();
                    break;
                case "Delete":
                    await apiService.DeleteShiftLog();
                    break;
                case "Update":
                    await apiService.UpdateShiftLog();
                    break;
                case "GetAll":
                    await apiService.DisplayLogs();
                    break;
                case "GetOne":
                    await apiService.GetShiftLog();
                    break;
                case "Exit":
                    Environment.Exit(0);
                    break;
            }

        }

        public static async Task Init()
        {
            bool runApp =  true;
            UserInput userInput = new UserInput();
            do
            {
               await PerformOperation();
                if (!userInput.ConfirmAction("do you want to perform another action"))
                {
                    runApp = false;
                }
            } while(runApp);
        }
    }
}
