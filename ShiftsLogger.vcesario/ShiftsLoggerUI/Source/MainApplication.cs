using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.Json;

using Spectre.Console;

public class MainApplication
{
    enum MenuOption
    {
        [Display(Name = AppTexts.OPTION_VIEWALL)]
        ViewAll,
        [Display(Name = AppTexts.OPTION_ADDSHIFT)]
        AddShift,
        [Display(Name = AppTexts.OPTION_EDITSHIFT)]
        Edit,
        [Display(Name = AppTexts.OPTION_REMOVESHIFT)]
        Remove,
        [Display(Name = AppTexts.OPTION_EXIT)]
        Exit,
    }

    private List<ShiftRecord>? m_AllShifts;

    public async Task Run()
    {
        await UpdateAllShifts();

        MenuOption chosenOption;
        do
        {
            Console.Clear();

            AnsiConsole.MarkupLine("[darkmagenta]Shifts Logger[/]");

            Console.WriteLine();
            PrintMostRecentShifts();

            Console.WriteLine();
            chosenOption = AnsiConsole.Prompt(
               new SelectionPrompt<MenuOption>()
               .Title(AppTexts.PROMPT_ACTION)
               .AddChoices(Enum.GetValues<MenuOption>())
               .UseConverter(GetEnumDisplayName)
           );

            switch (chosenOption)
            {
                case MenuOption.ViewAll:
                    ViewAllShifts();
                    break;
                case MenuOption.AddShift:
                    if (await TryAddNewShift())
                    {
                        await UpdateAllShifts();
                    }
                    break;
                case MenuOption.Edit:
                    if (await TryEditShift())
                    {
                        await UpdateAllShifts();
                    }
                    break;
                case MenuOption.Remove:
                    if (await TryRemoveShift())
                    {
                        await UpdateAllShifts();
                    }
                    break;
                case MenuOption.Exit:
                    break;
                default:
                    Console.WriteLine("Implement option: " + GetEnumDisplayName(chosenOption));
                    Console.ReadLine();
                    break;
            }
        }
        while (chosenOption != MenuOption.Exit);
    }

    private void ViewAllShifts()
    {
        Console.Clear();
        AnsiConsole.MarkupLine($"[darkmagenta]{AppTexts.LABEL_APPTITLE}[/] | {AppTexts.OPTION_VIEWALL}");

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

    private async Task<bool> TryAddNewShift()
    {
        Console.Clear();
        AnsiConsole.MarkupLine($"[darkmagenta]{AppTexts.LABEL_APPTITLE}[/] | {AppTexts.OPTION_ADDSHIFT}");
        AnsiConsole.MarkupLine($"[grey]{AppTexts.TOOLTIP_CANCEL}[/]");

        Console.WriteLine();
        if (!TryPromptNewShiftDto(false, out var shiftDto))
        {
            return false;
        }

        using (var client = new HttpClient())
        {
            string url = "https://localhost:7225/api/shiftlog";
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
                return false;
            }
        }

        Console.WriteLine(AppTexts.LOG_NEWSHIFT_SUCCESS);
        Console.ReadLine();
        return true;
    }

    private async Task<bool> TryEditShift()
    {
        Console.Clear();
        AnsiConsole.MarkupLine($"[darkmagenta]{AppTexts.LABEL_APPTITLE}[/] | {AppTexts.OPTION_EDITSHIFT}");
        AnsiConsole.MarkupLine($"[grey]{AppTexts.TOOLTIP_CANCEL}[/]");

        Console.WriteLine();
        UserInputValidator validator = new();
        string idString = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter ID to edit:")
            .Validate(validator.ValidateIdOrPeriod)
        );

        if (idString.Equals("."))
        {
            return false;
        }

        var shift = await GetShift(idString);
        if (shift == null)
        {
            return false;
        }

        DrawShiftTable(new List<ShiftRecord> { shift });

        Console.WriteLine();
        if (!TryPromptNewShiftDto(true, out var shiftDto)
            || shiftDto == null)
        {
            return false;
        }

        var updatedRecord = new ShiftRecord(shift.id, shiftDto.WorkerId, shiftDto.StartDateTime, shiftDto.EndDateTime);
        using (var client = new HttpClient())
        {
            string url = $"https://localhost:7225/api/shiftlog";
            var serializedContent = JsonSerializer.Serialize(updatedRecord);
            StringContent content = new StringContent(serializedContent, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PutAsync(url, content);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine();
                Console.WriteLine("HTTP Error: " + e.HttpRequestError);
                Console.WriteLine(e.Message);
                Console.ReadLine();
                return false;
            }
        }

        Console.WriteLine("Shift updated successfully.");
        Console.ReadLine();

        return true;
    }

    private async Task<bool> TryRemoveShift()
    {
        Console.Clear();
        AnsiConsole.MarkupLine($"[darkmagenta]{AppTexts.LABEL_APPTITLE}[/] | {AppTexts.OPTION_REMOVESHIFT}");
        AnsiConsole.MarkupLine($"[grey]{AppTexts.TOOLTIP_CANCEL}[/]");

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

        var shift = await GetShift(idString);
        if (shift == null)
        {
            return false;
        }

        DrawShiftTable(new List<ShiftRecord> { shift });

        Console.WriteLine();
        var confirm = AnsiConsole.Prompt(
            new ConfirmationPrompt("Are you sure you want to delete this entry?")
            {
                DefaultValue = false
            });

        if (!confirm)
        {
            return false;
        }

        confirm = AnsiConsole.Prompt(
            new ConfirmationPrompt(AppTexts.PROMPT_RECONFIRM)
            {
                DefaultValue = false
            });

        if (!confirm)
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
                Console.ReadLine();
                return false;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Entry '{idString}' successfully removed.");

        Console.WriteLine();
        AnsiConsole.Markup($"[grey]{AppTexts.TOOLTIP_RETURN}[/]");
        Console.ReadLine();
        return true;
    }

    private bool TryPromptNewShiftDto(bool edit, out ShiftDto_WithoutId? newShift)
    {
        UserInputValidator validator = new();

        var workerIdInput = AnsiConsole.Prompt(
            new TextPrompt<string>(edit ? "Enter new Worker ID:" : AppTexts.PROMPT_NEWSHIFT_WORKERID)
            .Validate(validator.ValidateIdOrPeriod)
        );
        if (workerIdInput.Equals("."))
        {
            newShift = null;
            return false;
        }
        int workerId = int.Parse(workerIdInput);

        Console.WriteLine();
        DateTime startDatetime;
        do
        {
            var startDatetimeInput = AnsiConsole.Prompt(
                new TextPrompt<string>(edit ? "Enter new start time:" : AppTexts.PROMPT_NEWSHIFT_STARTDATETIME)
                .Validate(validator.ValidateDatetimeOrPeriod)
            );
            if (startDatetimeInput.Equals("."))
            {
                newShift = null;
                return false;
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
                new TextPrompt<string>(edit ? "Enter new end time:" : AppTexts.PROMPT_NEWSHIFT_ENDDATETIME)
                .Validate(validator.ValidateDatetimeOrPeriod)
            );
            if (endDatetimeInput.Equals("."))
            {
                newShift = null;
                return false;
            }

            endDatetime = DateTime.ParseExact(endDatetimeInput, AppTexts.FORMAT_DATETIME, CultureInfo.InvariantCulture);
            if (endDatetime >= DateTime.Now || endDatetime <= startDatetime)
            {
                Console.WriteLine(AppTexts.ERROR_BADENDDATETIME);
                continue;
            }
        }
        while (false);

        newShift = new ShiftDto_WithoutId(workerId, startDatetime, endDatetime);
        return true;
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

    private async Task<ShiftRecord?> GetShift(string id)
    {
        ShiftRecord? shift = null;

        if (string.IsNullOrEmpty(id))
        {
            return null;
        }

        using (var client = new HttpClient())
        {
            string url = $"https://localhost:7225/api/shiftlog/{id}";
            try
            {
                using (var stream = await client.GetStreamAsync(url))
                {
                    shift = JsonSerializer.Deserialize<ShiftRecord>(stream);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine();
                Console.WriteLine("HTTP Error: " + e.HttpRequestError);
                Console.WriteLine(e.Message);
                Console.ReadLine();

                return null;
            }
        }

        if (shift == null)
        {
            Console.WriteLine();
            Console.WriteLine("No shift found with this ID.");
            Console.ReadLine();
        }

        return shift;
    }

    private void PrintMostRecentShifts()
    {
        if (m_AllShifts == null || m_AllShifts.Count == 0)
        {
            return;
        }

        int count = Math.Min(3, m_AllShifts.Count);
        var latestShifts = m_AllShifts.GetRange(0, count);

        DrawShiftTable(latestShifts);
    }

    private string GetEnumDisplayName(MenuOption option)
    {
        string? name = option.GetType()?.GetMember(option.ToString())?.First()?
                        .GetCustomAttribute<DisplayAttribute>()?.Name;

        return string.IsNullOrEmpty(name) ? string.Empty : name;
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