using System.Text.Json;
using Spectre.Console;

public class ShiftsManager
{
    private enum MenuOption
    {
        ViewAll,
        Edit,
        Remove,
        Return,
    }

    private MainApplication m_MainApp;
    private List<ShiftRecord>? m_AllShifts;

    public ShiftsManager(MainApplication mainApp)
    {
        m_MainApp = mainApp;
    }

    public async Task Open()
    {
        await UpdateAllShifts();

        MenuOption chosenOption;
        do
        {
            Console.Clear();

            m_MainApp.PrintHeader();

            Console.WriteLine();
            PrintLatestShifts();

            Console.WriteLine();
            chosenOption = AnsiConsole.Prompt(
               new SelectionPrompt<MenuOption>()
               .Title(AppTexts.PROMPT_ACTION)
               .AddChoices(Enum.GetValues<MenuOption>())
           );

            switch (chosenOption)
            {
                case MenuOption.ViewAll:
                    ViewAllShifts();
                    break;
                case MenuOption.Remove:
                    if (await TryRemoveShift())
                    {
                        await UpdateAllShifts();
                    }
                    break;
                case MenuOption.Return:
                    break;
                default:
                    Console.WriteLine("Implement option: " + chosenOption);
                    Console.ReadLine();
                    break;
            }
        }
        while (chosenOption != MenuOption.Return);
    }

    private async Task UpdateAllShifts()
    {
        using (var client = new HttpClient())
        {
            string url = "https://localhost:7225/api/shiftlog";
            try
            {
                using (var stream = await client.GetStreamAsync(url))
                {
                    m_AllShifts = JsonSerializer.Deserialize<List<ShiftRecord>>(stream);
                }

                m_AllShifts?.Sort((s1, s2) => s2.startDateTime.CompareTo(s1.startDateTime));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine();
                Console.WriteLine("HTTP Error: " + e.HttpRequestError);
                Console.WriteLine(e.Message);
                Console.ReadLine();

                m_AllShifts = null;
            }
        }
    }

    private void ViewAllShifts()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[darkmagenta]Shifts Logger[/] View all shifts");

        if (m_AllShifts == null || m_AllShifts.Count == 0)
        {
            Console.WriteLine();
            Console.WriteLine("No shifts were found in the database.");
            Console.ReadLine();
            return;
        }

        Console.WriteLine();
        DrawShiftTable(m_AllShifts);

        Console.WriteLine();
        AnsiConsole.Markup("[grey]Press any key to return.[/]");
        Console.ReadLine();
    }

    private async Task<bool> TryRemoveShift()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[darkmagenta]Shifts Logger[/] Remove shift");

        Console.WriteLine();
        UserInputValidator validator = new();
        string idString = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter ID to remove:")
            .Validate(validator.ValidateIdOrPeriod)
        );

        if (idString.Equals("."))
        {
            return false;
        }

        using (var client = new HttpClient())
        {
            string url = $"https://localhost:7225/api/shiftlog/{idString}";
            try
            {
                var response = await client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine();
                Console.WriteLine("HTTP Error: " + e.HttpRequestError);
                Console.WriteLine(e.Message);
                AnsiConsole.MarkupLine("[indianred]Check if the ID was entered correctly.[/]");
                Console.ReadLine();
                return false;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Entry '{idString}' successfully removed.");
        
        Console.WriteLine();
        AnsiConsole.Markup("[grey]Press any key to return.[/]");
        Console.ReadLine();
        return true;
    }

    private void PrintLatestShifts()
    {
        if (m_AllShifts == null || m_AllShifts.Count == 0)
        {
            return;
        }

        int count = Math.Min(3, m_AllShifts.Count);
        var latestShifts = m_AllShifts.GetRange(0, count);

        DrawShiftTable(latestShifts);
    }

    private void DrawShiftTable(List<ShiftRecord> shifts)
    {
        var table = new Table();

        table.AddColumns(
            $"[indianred]{AppTexts.LABEL_SHIFTTABLE_ID}[/]",
            $"[indianred]{AppTexts.LABEL_SHIFTTABLE_WORKERID}[/]",
            $"[indianred]{AppTexts.LABEL_SHIFTTABLE_STARTTIME}[/]",
            $"[indianred]{AppTexts.LABEL_SHIFTTABLE_ENDTIME}[/]"
        );

        foreach (var shift in shifts)
        {
            table.AddRow(
                shift.id.ToString(),
                shift.workerId.ToString(),
                shift.startDateTime.ToString("R"),
                shift.endDateTime.ToString("R")
            );
        }

        table.Border = TableBorder.Double;
        AnsiConsole.Write(table);
    }
}