using shiftloggerconsole.Services;
using Spectre.Console;
using static shiftloggerconsole.Helpers.Helpers;
using static shiftloggerconsole.UserInterface.Enums;

namespace shiftloggerconsole.UserInterface;

internal static class MainMenu
{
    internal static async Task ShowMenu(IHttpClientFactory httpClientFactory, Microsoft.Extensions.Configuration.IConfigurationRoot? config)
    {
        var httpClient = httpClientFactory.CreateClient();
        string? apiBaseUrl = config["ApiSettings:BaseUrl"];
        string? endPointUrl = config["ApiSettings:EndpointUrl"];

        var shiftLoggerService = new ShiftLoggerService(httpClientFactory.CreateClient("ShiftLoggerApi"), apiBaseUrl, endPointUrl);

        var appIsRunning = true;
        while (appIsRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOptions>()
                .Title("What would you like to do?")
                .AddChoices(
                    MenuOptions.AddShift,
                    MenuOptions.ShowAllShifts,
                    MenuOptions.EditShiftById,
                    MenuOptions.DeleteShiftById,
                    MenuOptions.DevelopersDisclaimer,
                    MenuOptions.Quit
                    ));

            switch (option)
            {
                case MenuOptions.AddShift:
                    await shiftLoggerService.InsertShiftAsync();
                    break;
                case MenuOptions.ShowAllShifts:
                    await shiftLoggerService.GetAllShifts();
                    break;
                case MenuOptions.EditShiftById:
                    await shiftLoggerService.EditShift();
                    break;
                case MenuOptions.DeleteShiftById:
                    await shiftLoggerService.DeleteShiftById();
                    break;
                case MenuOptions.DevelopersDisclaimer:
                    DevelopersNote();
                    break;
                case MenuOptions.Quit:
                    appIsRunning = false;
                    Environment.Exit(0);
                    break;

            }
        }
    }
}
