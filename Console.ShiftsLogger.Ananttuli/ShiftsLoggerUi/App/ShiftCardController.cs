using ShiftsLoggerUi.Api;
using ShiftsLoggerUi.Api.Shifts;
using ShiftsLoggerUi.App.Utils;
using Spectre.Console;

namespace ShiftsLoggerUi.App;

public class ShiftCardController
{
    public ShiftsApi ShiftsApi { get; set; }

    public ShiftCardController(ShiftsApi shiftsApi)
    {
        ShiftsApi = shiftsApi;
    }

    public async Task OpenShiftCard(ShiftDto shift)
    {
        var keepCardOpen = true;

        while (keepCardOpen)
        {
            var shiftsTable = new Table();

            shiftsTable.AddColumns(["Start time", "End time", "Duration"]);

            shiftsTable.AddRow(GetShiftRow(shift));

            AnsiConsole.Write(shiftsTable);

            keepCardOpen = await ShowShiftCardOps(shift);
        }
    }

    public async Task<bool> ShowShiftCardOps(ShiftDto shift)
    {
        const string backButton = "Back";
        const string editShift = "Edit Shift";
        const string deleteShift = "Delete Shift";

        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices([
                    backButton,
                    editShift,
                    deleteShift
                ])
        );

        switch (option)
        {
            case backButton:
                Console.Clear();
                return false;
            case editShift:
                await UpdateShift(shift.ShiftId);
                break;
            case deleteShift:
                await DeletedShift(shift.ShiftId);
                return false;
            default:
                Console.Clear();
                return false;
        }

        ConsoleUtil.PressAnyKeyToClear();
        return true;
    }

    public async Task UpdateShift(int shiftId)
    {
        AnsiConsole.MarkupLine($"\nUpdate shift ID {shiftId}:");

        var (startDateTime, endDateTime) = ShiftsController.PromptShiftTimes();

        var result = await ShiftsApi.UpdateShift(
            new ShiftUpdateDto(shiftId, startDateTime, endDateTime)
        );

        if (result.Success)
        {
            AnsiConsole.MarkupLine(
                $"[green]Shift updated[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"{result?.Message ?? "Error"}");
        }
    }
    private async Task DeletedShift(int id)
    {
        var deleteResult = await ShiftsApi.DeleteShift(id);
        AnsiConsole.MarkupLine(deleteResult.Success ?
            "Deleted" :
            deleteResult.Message ?? "Error"
        );
    }

    public static string[] GetShiftRow(ShiftDto shift)
    {
        return [
            shift.StartTime.ToString(),
            shift.EndTime.ToString(),
            Time.Duration(shift.StartTime, shift.EndTime)
        ];
    }

}