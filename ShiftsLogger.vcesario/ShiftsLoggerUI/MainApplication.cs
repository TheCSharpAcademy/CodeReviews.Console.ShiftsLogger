using System.Globalization;
using Newtonsoft.Json;
using Spectre.Console;

public class MainApplication
{
    enum MenuOption
    {
        NewShift,
        ManageShifts,
        Exit,
    }

    public async Task Run()
    {
        // DataService.Initialize();

        MenuOption chosenOption;
        do
        {
            Console.Clear();

            AnsiConsole.MarkupLine($"[darkmagenta]{AppTexts.LABEL_APPTITLE}[/]");

            Console.WriteLine();
            chosenOption = AnsiConsole.Prompt(
               new SelectionPrompt<MenuOption>()
               .Title(AppTexts.PROMPT_ACTION)
               .AddChoices(Enum.GetValues<MenuOption>())
               .UseConverter(ConvertMenuOption)
           );

            switch (chosenOption)
            {
                case MenuOption.NewShift:
                    AddNewShift();
                    break;
                case MenuOption.ManageShifts:
                    break;
                case MenuOption.Exit:
                    break;
                default:
                    Console.WriteLine("Implement option: " + chosenOption);
                    Console.ReadLine();
                    break;
            }
        }
        while (chosenOption != MenuOption.Exit);
    }

    private void AddNewShift()
    {
        Console.Clear();
        Console.WriteLine(AppTexts.LABEL_MAINMENU_NEWSHIFT);
        AnsiConsole.MarkupLine($"[grey]{AppTexts.TOOLTIP_CANCEL}[/]");

        UserInputValidator validator = new();

        Console.WriteLine();
        var workerIdInput = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your worker ID:")
            .Validate(validator.ValidateWorkerIdOrPeriod)
        );
        if (workerIdInput.Equals("."))
        {
            return;
        }
        int workerId = int.Parse(workerIdInput);

        Console.WriteLine();
        DateTime startDatetime;
        do
        {
            var startDatetimeInput = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter the start date and time of your shift:")
                .Validate(validator.ValidateDatetimeOrPeriod)
            );
            if (startDatetimeInput.Equals("."))
            {
                return;
            }

            startDatetime = DateTime.ParseExact(startDatetimeInput, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            if (startDatetime >= DateTime.Now)
            {
                Console.WriteLine("Invalid date time for starting a shift.");
                continue;
            }
        }
        while (false);

        Console.WriteLine();
        DateTime endDatetime;
        do
        {
            var endDatetimeInput = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter the start date and time of your shift:")
                .Validate(validator.ValidateDatetimeOrPeriod)
            );
            if (endDatetimeInput.Equals("."))
            {
                return;
            }

            endDatetime = DateTime.ParseExact(endDatetimeInput, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            if (endDatetime >= DateTime.Now)
            {
                Console.WriteLine("Invalid date time for starting a shift.");
                continue;
            }
        }
        while (false);

        using (var client = new HttpClient())
        {
            string url = "https://localhost:7225/api/shiftlog";
            var shiftDto = new ShiftDto_WithoutId(workerId, startDatetime, endDatetime);
            StringContent content = new StringContent(JsonConvert.SerializeObject(shiftDto));
            client.PostAsync(url, content);

            // ...
        }
    }

    private string ConvertMenuOption(MenuOption option)
    {
        switch (option)
        {
            case MenuOption.NewShift:
                return AppTexts.LABEL_MAINMENU_NEWSHIFT;
            case MenuOption.ManageShifts:
                return AppTexts.LABEL_MAINMENU_MANAGESHIFTS;
            case MenuOption.Exit:
                return AppTexts.LABEL_EXIT;
            default:
                return AppTexts.LABEL_UNDEFINED;
        }
    }
}