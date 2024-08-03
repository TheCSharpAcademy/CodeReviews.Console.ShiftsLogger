using Models;
using ShiftsLogger.Services;

namespace ShiftsLogger.Controllers;

public class ShiftController
{
    private readonly ApiService _service;
    public static readonly List<string> Options = ["View shifts", "Add shift", "Edit shift", "Delete shift"];
    private readonly Dictionary<string, Action> _optionHandlers;


    public ShiftController(ApiService service)
    {
        _service = service;
        _optionHandlers = new Dictionary<string, Action>{
            { Options[0], ViewShifts },
            { Options[1], AddShift },
            { Options[2], EditShift },
            { Options[3], DeleteShift }
        };
    }

    public void HandleChoice(string choice)
    {
        _optionHandlers.TryGetValue(choice, out var action);
        action!();
    }

    private void ViewShifts()
    {
        List<GetShiftDto>? shifts;

        try
        {
            shifts = _service.GetAllShifts();
        }
        catch (Exception e)
        {
            UI.ConfirmationMessage(e.Message);
            return;
        }

        if (shifts == null)
        {
            UI.ConfirmationMessage("No shifts to view");
            return;
        }

        UI.DisplayShifts(shifts);
        UI.ConfirmationMessage("");
    }

    private void AddShift()
    {
        var name = UI.StringResponse("Enter the [green]name[/] of the shift");
        var startTime = UI.TimeOnlyResponse("Enter the [green]start time[/] of the shift");
        var endTime = UI.TimeOnlyResponse("Enter the [green]end time[/] of the shift");

        var newShift = new PostShiftDto
        {
            Name = name,
            StartTime = startTime,
            EndTime = endTime
        };

        try
        {
            _service.CreateShift(newShift);

            UI.ConfirmationMessage("Shift created");
        }
        catch (Exception e)
        {
            UI.ConfirmationMessage($"Error creating shift - {e.Message}");
        }
    }

    private void EditShift()
    {
        List<GetShiftDto>? shifts;

        try
        {
            shifts = _service.GetAllShifts();
        }
        catch (Exception e)
        {
            UI.ConfirmationMessage(e.Message);
            return;
        }

        if (shifts == null)
        {
            UI.ConfirmationMessage("No shifts to view");
            return;
        }

        UI.DisplayShifts(shifts);
        GetShiftDto shift = SelectShiftFromList(shifts, "edit");

        var name = UI.StringResponseWithDefault($"Enter the [green]shift name[/]", shift.Name);
        var startTime = UI.TimeOnlyResponseWithDefault("Enter the [green]shift start time[/]", shift.StartTime);
        var endTime = UI.TimeOnlyResponseWithDefault("Enter the [green]shift end time[/]", shift.EndTime);

        var dto = new PutShiftDto
        {
            Name = name,
            StartTime = startTime,
            EndTime = endTime
        };

        try
        {
            _service.UpdateShift(shift.Id, dto);
        }
        catch (Exception e)
        {
            UI.ConfirmationMessage("Error updating shift: " + e.Message);
        }

        UI.ConfirmationMessage("Updated shift");
    }

    private void DeleteShift()
    {
        List<GetShiftDto>? shifts;

        try
        {
            shifts = _service.GetAllShifts();
        }
        catch (Exception e)
        {
            UI.ConfirmationMessage(e.Message);
            return;
        }

        if (shifts == null)
        {
            UI.ConfirmationMessage("No shifts to view");
            return;
        }

        UI.DisplayShifts(shifts);
        GetShiftDto shift = SelectShiftFromList(shifts, "delete");

        try
        {
            _service.DeleteShift(shift.Id);
        }
        catch (Exception e)
        {
            UI.ConfirmationMessage(e.Message);
        }
        UI.ConfirmationMessage("Shift deleted");
    }

    public static GetShiftDto SelectShiftFromList(List<GetShiftDto> shifts, string action)
    {
        GetShiftDto? shift = null;
        while (shift == null)
        {
            var id = UI.IntResponse($"Enter the [green]id[/] of the shift you wish to {action}");
            shift = shifts.FirstOrDefault(s => s.Id == id);

            if (shift == null)
            {
                UI.InvalidationMessage("No shift with that id");
            }
        }
        return shift;
    }
}