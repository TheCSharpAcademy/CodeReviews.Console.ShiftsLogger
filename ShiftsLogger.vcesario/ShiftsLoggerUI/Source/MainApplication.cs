using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

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

    private List<ShiftRecord> m_AllShifts = new();

    public async Task Run()
    {
        await UpdateAllShifts();

        MenuOption chosenOption;
        do
        {
            Console.Clear();

            AnsiConsole.MarkupLine($"[darkmagenta]{AppTexts.LABEL_APPTITLE}[/]");

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
        if (!TryPromptNewShiftDto(false, out var shiftDto)
            || shiftDto == null)
        {
            return false;
        }

        ApiHandler apiHandler = new();
        if (await apiHandler.PostShift(shiftDto) == false)
        {
            return false;
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

        ApiHandler apiHandler = new();
        var shift = await apiHandler.GetShift(idString);
        if (shift == null)
        {
            Console.WriteLine();
            Console.WriteLine("No shift found with this ID.");
            Console.ReadLine();

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
        if (await apiHandler.PutShift(updatedRecord) == false)
        {
            return false;
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

        ApiHandler apiHandler = new();
        var shift = await apiHandler.GetShift(idString);
        if (shift == null)
        {
            Console.WriteLine();
            Console.WriteLine("No shift found with this ID.");
            Console.ReadLine();

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

        if (await apiHandler.DeleteShift(idString) == false)
        {
            return false;
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
        ApiHandler apiHandler = new();
        m_AllShifts = await apiHandler.GetAllShifts();
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