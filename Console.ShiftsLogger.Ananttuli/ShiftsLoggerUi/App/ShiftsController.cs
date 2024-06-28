using System.Globalization;
using ShiftsLoggerUi.Api;
using ShiftsLoggerUi.Api.Shifts;
using ShiftsLoggerUi.App.Utils;
using Spectre.Console;

namespace ShiftsLoggerUi.App;

public class ShiftsController
{
    public EmployeesApi EmployeesApi { get; set; }
    public ShiftsApi ShiftsApi { get; set; }
    public ShiftCardController ShiftCardController { get; set; }

    public ShiftsController(ShiftsApi shiftsApi, EmployeesApi employeesApi)
    {
        this.ShiftsApi = shiftsApi;
        this.EmployeesApi = employeesApi;
        this.ShiftCardController = new ShiftCardController(shiftsApi);
    }

    public async Task ManageAllShifts()
    {
        var keepManageMenuOpen = true;

        while (keepManageMenuOpen)
        {
            var (success, shifts) = await ShiftsApi.GetShifts();

            if (!success || shifts == null)
            {
                return;
            }

            keepManageMenuOpen = await ManageShifts(shifts);
        }
    }

    public async Task<bool> ManageShifts(
        List<ShiftDto> shifts
    )
    {
        var selectedShift = SelectShift(shifts);

        if (selectedShift == null)
        {
            return false;
        }

        await ShiftCardController.OpenShiftCard(selectedShift);

        return true;
    }

    public ShiftDto? SelectShift(List<ShiftDto> shifts)
    {
        var backButton = new ShiftDto(
            -1,
            DateTime.Now,
            DateTime.Now,
            new Api.Employees.EmployeeCoreDto(-1, "")
        );

        var selectedShift = AnsiConsole.Prompt(
            new SelectionPrompt<ShiftDto>()
                .Title("S H I F T S")
                .UseConverter(item =>
                {
                    var isBackButton = backButton.Equals(item);

                    if (isBackButton)
                    {
                        return "[red]<- Back[/]";
                    }

                    List<string> parts = [
                        item.ShiftId.ToString(),
                        item.StartTime.ToString(),
                        item.EndTime.ToString(),
                        Time.Duration(item.StartTime, item.EndTime),
                        item.Employee.Name
                    ];

                    return string.Join("    ", parts.Select(p => $"{p,-5}"));
                })
                .AddChoices([
                    backButton,
                    ..shifts
                ])
                .EnableSearch()
        );

        if (
            selectedShift == null ||
            selectedShift.Equals(backButton)
        )
        {
            return null;
        }

        return selectedShift;
    }

    public async Task CreateShift(int employeeId)
    {
        AnsiConsole.MarkupLine($"\nCreating shift for Employee ID {employeeId}:");

        var (startDateTime, endDateTime) = PromptShiftTimes();

        var result = await ShiftsApi.CreateShift(
            new ShiftCreateDto(employeeId, startDateTime, endDateTime)
        );

        if (result.Success)
        {

            AnsiConsole.MarkupLine(
                $"[green]Shift created[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"{result?.Message ?? "Error"}");
        }
    }

    public static Tuple<DateTime, DateTime> PromptShiftTimes()
    {
        const string expectedDateTimeFormat = "yyyy-MM-dd HH:mm";

        AnsiConsole.MarkupLine("[grey]Note: Date times must be YYYY-mm-dd hh:mm with 24hr time e.g. [/][blue]2024-12-31 14:15[/]");

        string validStartDateTimeString = AnsiConsole.Prompt(
            new TextPrompt<string>("[green]Start[/] date time: ")
                .PromptStyle("blue")
                .ValidationErrorMessage("[red]That's not a valid date time[/]")
                .Validate(startDateTimeInput =>
                    {
                        bool validDate = DateTime.TryParseExact(
                           startDateTimeInput,
                           expectedDateTimeFormat,
                           CultureInfo.CurrentCulture,
                           DateTimeStyles.None,
                           out DateTime value
                       );

                        return validDate switch
                        {
                            false => ValidationResult.Error("\t[red]Please enter valid date format[/]"),
                            true => ValidationResult.Success()
                        };
                    }
                )
        );

        DateTime startDateTime = DateTime.Parse(validStartDateTimeString);

        string validEndDateTimeString = AnsiConsole.Prompt(
            new TextPrompt<string>("[green]End[/] date time: ")
                .PromptStyle("blue")
                .ValidationErrorMessage("[red]That's not a valid date time[/]")
                .Validate(endDateTimeInput =>
                    {
                        bool validDate = DateTime.TryParseExact(
                           endDateTimeInput,
                           expectedDateTimeFormat,
                           CultureInfo.CurrentCulture,
                           DateTimeStyles.None,
                           out DateTime value
                       );

                        if (validDate == false)
                        {
                            return ValidationResult.Error("\t[red]Please enter valid date format[/]");
                        }

                        if (value < startDateTime)
                        {
                            return ValidationResult.Error("\t[red]End date time must be later than start date time[/]");
                        }

                        return ValidationResult.Success();
                    }
                )
        );

        DateTime endDateTime = DateTime.Parse(validEndDateTimeString);

        return new Tuple<DateTime, DateTime>(startDateTime, endDateTime);
    }
}