using ShiftLogger.Mefdev.ShiftLoggerUI.Dtos;
using ShiftLogger.Mefdev.ShiftLoggerUI.Inputs;
using ShiftLogger.Mefdev.ShiftLoggerUI.Services;
using Spectre.Console;

namespace ShiftLogger.Mefdev.ShiftLoggerUI.Controllers;

public class WorkerShiftController: WorkerShiftBase
{
    private readonly ManageShifts _manageShift;
    private readonly UserInput _userInput;

    public WorkerShiftController(ManageShifts manageShift, UserInput userInput)
	{
        _manageShift = manageShift;
        _userInput = userInput;
    }

    public async Task CreateShift()
    {
        int id = int.Parse(_userInput.GetId());
        var oldWorkerShift = await _manageShift.GetWorkerShift(id);
        if (oldWorkerShift is not null)
        {
            DisplayMessage("The shift is already exists", "red");
            AnsiConsole.Confirm("Press any key to continue... ");
            return;
        }
        string name = _userInput.GetName();
        DateTime startDate = _userInput.GetDate();
        DateTime endDate = _userInput.GetDate();

        var data = await _manageShift.CreateWorkerShift(new WorkerShiftDto(id, name, startDate, endDate));
        if (data is not null)
        {
            DisplayMessage("The shift is created", "green");
        }
        AnsiConsole.Confirm("Press any key to continue... ");
    }

    public async Task GetShifts()
    {
        var workerShifts = await _manageShift.GetWorkerShifts();
        if (workerShifts.Count < 1 && workerShifts == null)
        {
            DisplayMessage("Nothing to show the data is empty", "red");
            AnsiConsole.Confirm("Press any key to continue... ");
            return;
        }
        DisplayAllItems(workerShifts); 
    }

    public async Task GetShift()
    {
        int id = int.Parse(_userInput.GetId());
        var workerShift = await _manageShift.GetWorkerShift(id);
        if (workerShift is null)
        {
            DisplayMessage("Nothing to show or the data is empty", "red");
            AnsiConsole.Confirm("Press any key to continue... ");
            return;
        }
        DisplayItemTable(workerShift);
    }

    public async Task DeleteShift()
    {
        int id = int.Parse(_userInput.GetId());
        var isDeleted = await _manageShift.DeleteWorkerShift(id);
        if(!isDeleted)
        {
            DisplayMessage("The shift you're looking for is not found", "red");
            AnsiConsole.Confirm("Press any key to continue... ");
            return;
        }
        DisplayMessage("The shift has been deleted", "green");
        AnsiConsole.Confirm("Press any key to continue... ");
    }

    public async Task UpdateShift()
    {
        int id = int.Parse(_userInput.GetId());
        var oldWorkerShift = await _manageShift.GetWorkerShift(id);
        if (oldWorkerShift is null)
        {
            DisplayMessage("Nothing to show or the data is empty", "red");
            AnsiConsole.Confirm("Press any key to continue... ");
            return;
        }
        string name = _userInput.GetName(oldWorkerShift.Name);
        DateTime startDate = _userInput.GetDate(oldWorkerShift.Start.ToString());
        DateTime endDate = _userInput.GetDate(oldWorkerShift.End.ToString());

        var shift = await _manageShift.UpdateWorkerShift(id, new WorkerShiftDto(oldWorkerShift.Id, name, startDate, endDate));
        if (shift is not null)
        {
            DisplayMessage("The shift is updated", "green");
        }
        AnsiConsole.Confirm("Press any key to continue... ");
    }
}