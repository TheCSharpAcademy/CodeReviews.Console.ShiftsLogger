using System.Globalization;
using System.Text;
using System.Text.Json;

// using Newtonsoft.Json;
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
        MenuOption chosenOption;
        do
        {
            Console.Clear();

            PrintHeader();

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
                    await AddNewShift();
                    break;
                case MenuOption.ManageShifts:
                    await new ShiftsManager(this).Open();
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

    public void PrintHeader()
    {
        AnsiConsole.MarkupLine($"[darkmagenta]{AppTexts.LABEL_APPTITLE}[/]");
    }

    private async Task AddNewShift()
    {
        Console.Clear();
        Console.WriteLine(AppTexts.OPTION_MAINMENU_NEWSHIFT);
        AnsiConsole.MarkupLine($"[grey]{AppTexts.TOOLTIP_CANCEL}[/]");

        UserInputValidator validator = new();

        Console.WriteLine();
        var workerIdInput = AnsiConsole.Prompt(
            new TextPrompt<string>(AppTexts.PROMPT_NEWSHIFT_WORKERID)
            .Validate(validator.ValidateIdOrPeriod)
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
                new TextPrompt<string>(AppTexts.PROMPT_NEWSHIFT_STARTDATETIME)
                .Validate(validator.ValidateDatetimeOrPeriod)
            );
            if (startDatetimeInput.Equals("."))
            {
                return;
            }

            startDatetime = DateTime.ParseExact(startDatetimeInput, AppTexts.FORMAT_DATETIME, CultureInfo.InvariantCulture);
            if (startDatetime >= DateTime.Now)
            {
                Console.WriteLine(AppTexts.ERROR_BADSTARTDATETIME);
                continue;
            }
        }
        while (false);

        Console.WriteLine();
        DateTime endDatetime;
        do
        {
            var endDatetimeInput = AnsiConsole.Prompt(
                new TextPrompt<string>(AppTexts.PROMPT_NEWSHIFT_ENDDATETIME)
                .Validate(validator.ValidateDatetimeOrPeriod)
            );
            if (endDatetimeInput.Equals("."))
            {
                return;
            }

            endDatetime = DateTime.ParseExact(endDatetimeInput, AppTexts.FORMAT_DATETIME, CultureInfo.InvariantCulture);
            if (endDatetime >= DateTime.Now || endDatetime <= startDatetime)
            {
                Console.WriteLine(AppTexts.ERROR_BADENDDATETIME);
                continue;
            }
        }
        while (false);

        using (var client = new HttpClient())
        {
            string url = "https://localhost:7225/api/shiftlog";
            var shiftDto = new ShiftDto_WithoutId(workerId, startDatetime, endDatetime);
            var serializedContent = JsonSerializer.Serialize(shiftDto);
            StringContent content = new StringContent(serializedContent, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine();
                Console.WriteLine("HTTP Error: " + e.HttpRequestError);
                Console.WriteLine(e.Message);
                Console.ReadLine();
                return;
            }
        }

        Console.WriteLine(AppTexts.LOG_NEWSHIFT_SUCCESS);
        Console.ReadLine();
    }

    private string ConvertMenuOption(MenuOption option)
    {
        switch (option)
        {
            case MenuOption.NewShift:
                return AppTexts.OPTION_MAINMENU_NEWSHIFT;
            case MenuOption.ManageShifts:
                return AppTexts.OPTION_MAINMENU_MANAGESHIFTS;
            case MenuOption.Exit:
                return AppTexts.OPTION_EXIT;
            default:
                return AppTexts.LABEL_UNDEFINED;
        }
    }

}