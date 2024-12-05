using Shared.Models;
using ShiftsLoggerUI.Controllers;
using ShiftsLoggerUI.Utilities;
using ShiftsLoggerUI.Views;

namespace ShiftsLoggerUI.Services;

public class ShiftService(ShiftController shiftController)
{
    private readonly ShiftController _shiftController = shiftController;
    internal List<Shift>? ViewAllShifts()
    {
        List<Shift>? shifts =  _shiftController.GetAllShifts();
        TableVisualisation.DisplayTable(shifts);
        return shifts;
    }

    internal void CreateShift()
    {
        if (Util.ReturnToMenu()) return;
        Shift newShift = ShiftExtensions.GetShift();
        _shiftController.CreateShift(newShift);
    }

    internal void UpdateShift()
    {
        if (Util.ReturnToMenu()) return;
        List<Shift> shifts = ViewAllShifts();
        if (shifts == null || shifts.Count == 0)
        {
            Util.AskUserToContinue();
            return;
        }
        Shift updateShift = GetShiftById();
        string updateChoice = UserInput.PromptUserForShiftProperty();
        
        switch (updateChoice)
        {
            case "start":
                UpdateShiftProperty(updateShift, startTime : true);
                break;
            case "end":
                UpdateShiftProperty(updateShift, endTime: true);
                break;
            case "both":
                UpdateShiftProperty(updateShift, startTime : true, endTime: true);
                break;
        }
    }

    internal void DeleteShift()
    {
        if (Util.ReturnToMenu()) return;
        List<Shift> shifts = ViewAllShifts();
        if (shifts == null || shifts.Count == 0)
        {
            Util.AskUserToContinue();
            return;
        }
        
        Shift shiftToDelete = GetShiftById();
        _shiftController.DeleteShift(shiftToDelete.Id);
    }
    
    private Shift GetShiftById()
    {
        List<Shift> shifts = _shiftController.GetAllShifts();
        Shift shiftToUpdate = UserInput.IdPromptToGetShift(shifts);
        return shiftToUpdate;
    }

    private void UpdateShiftProperty(Shift shift, bool startTime = false, bool endTime = false)
    {
        string parsedStartTime = shift.StartTime.ToString("yyyy-MM-dd HH:mm");
        string parsedEndTime = shift.EndTime.ToString("yyyy-MM-dd HH:mm");
        
        if (startTime) shift.StartTime = UserInput.DatePrompt("start", endTime: parsedEndTime);
        if (endTime) shift.EndTime = UserInput.DatePrompt("end", startTime: parsedStartTime);
        shift.ShiftTime = ShiftExtensions.GetShiftDuration(shift.StartTime, shift.EndTime);
        
        _shiftController.UpdateShift(shift);
    }
}