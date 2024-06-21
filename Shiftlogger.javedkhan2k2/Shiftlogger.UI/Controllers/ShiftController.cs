using Shiftlogger.UI.DTOs;
using Shiftlogger.UI.Services;

namespace Shiftlogger.UI.Controllers;

internal class ShiftController
{
    private readonly ShiftService _shiftService;
    private readonly WorkerService _workerService;

    public ShiftController()
    {
        _shiftService = new ShiftService();
        _workerService = new WorkerService();
    }

    internal async Task DisplayAllShifts()
    {
        var shifts = await GetAllShifts();
        if (shifts != null)
        {
            VisualizationEngine.DisplayShifts(shifts, "Showing All Shifts");
            VisualizationEngine.DisplayContinueMessage();
        }
        else
        {
            VisualizationEngine.DisplayFailureMessage("There is no shifts or the API is not running");
        }
    }

    internal async Task AddShift()
    {
        var workers = await _workerService.GetWorkers();
        if(workers == null)
        {
            VisualizationEngine.DisplayFailureMessage("If the API is running then there is no Worker Found.");
            return;
        }
        VisualizationEngine.DisplayWorkers(workers, "All Workers");
        var workerId = UserInput.GetIntInput();
        if(workerId == 0)
        {
            VisualizationEngine.DisplayCancelOperation();
            return;
        }
        var worker = await _workerService.GetWorkerById(workerId);
        
        if(worker == null)
        {
            VisualizationEngine.DisplayFailureMessage($"Worker with id: [green]{workerId}[/] not found.");
            return;
        }
        var shift = UserInput.GetNewShift();
        if(shift == null)
        {
            VisualizationEngine.DisplayCancelOperation();
            return;
        }
        shift.workerId = workerId;
        var addedshift = await _shiftService.PostShift(shift);
        if (addedshift == null)
        {
            VisualizationEngine.DisplayFailureMessage("Shift is not added to database.");
        }
        else
        {
            VisualizationEngine.DisplaySuccessMessage("Shift is added to database successfully.");
        }
    }


    internal async Task UpdateShift()
    {
        var shifts = await GetAllShifts();
        if(shifts == null)
        {
            VisualizationEngine.DisplayFailureMessage("There is no shifts or the API is not running");
            return;
        }
        VisualizationEngine.DisplayShifts(shifts, "Showing All Shifts");
        var shiftId = UserInput.GetIntInput();
        if(shiftId == 0)
        {
            VisualizationEngine.DisplayCancelOperation();
            return;
        }
        ShiftDto shift = await GetShiftById(shiftId);
        
        if(shift == null)
        {
            VisualizationEngine.DisplayFailureMessage($"Shift with id: [green]{shiftId}[/] not found.");
            return;
        }
        var workers = await _workerService.GetWorkers();
        if(workers == null)
        {
            VisualizationEngine.DisplayFailureMessage($"No Workers found. Please add workers and try again.");
            return;
        }
        if(!UserInput.UpdateShift(shift, workers)) 
        {
            VisualizationEngine.DisplayCancelOperation();
            return;
        }

        if(!await _shiftService.PutShift(shiftId, shift))
        {
            VisualizationEngine.DisplayFailureMessage("Shift is not updated.");
        }
        else
        {
            VisualizationEngine.DisplaySuccessMessage("Shift is updated.");
        }

    }

    internal async Task DeleteShift()
    {
        var shifts = await GetAllShifts();
        if(shifts == null)
        {
            VisualizationEngine.DisplayFailureMessage("There is no shifts or the API is not running");
            return;
        }
        VisualizationEngine.DisplayShifts(shifts, "Showing All Shifts");
        var shiftId = UserInput.GetIntInput();
        if(shiftId == 0)
        {
            VisualizationEngine.DisplayCancelOperation();
            return;
        }
        ShiftDto shift = await GetShiftById(shiftId);
        
        if(shift == null)
        {
            VisualizationEngine.DisplayFailureMessage($"Shift with id: [green]{shiftId}[/] not found.");
            return;
        }

        if(!await _shiftService.DeleteShift(shiftId))
        {
            VisualizationEngine.DisplayFailureMessage("Shift is not Deleted.");
        }
        else
        {
            VisualizationEngine.DisplaySuccessMessage("Shift is Deleted.");
        }
    }

    internal async Task<List<ShiftReqestDto>?> GetAllShifts() => await _shiftService.GetAllShifts();
    internal async Task<ShiftReqestDto?> GetShiftById(int id) => await _shiftService.GetShiftById(id);


}