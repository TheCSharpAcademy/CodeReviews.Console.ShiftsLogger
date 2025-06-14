
public class ManageShiftMenuController : MenuControllerBase
{
    WorkerService _workerService = new();
    ShiftService _shiftService;
    internal override async Task OnMakeAsync()
    {
        _workerService.ConnectApi();
        List<Worker> workers = await _workerService.GetAllWorkersAsync();
        Worker currentWorker = GetData.GetWorker(workers);

        _shiftService = new(currentWorker);

        _shiftService.ConnectApi();
    }
    internal override async Task<bool> HandleMenuSelectionAsync()
    {
        MenuEnums.Shift input = DisplayMenu.ShiftsMenu();
        switch (input)
        {
            case MenuEnums.Shift.CREATESHIFT:
                await CreateShiftAsync();
                break;
            case MenuEnums.Shift.READSHIFT:
                await ReadShiftAsync();
                break;
            case MenuEnums.Shift.UPDATESHIFT:
                await UpdateShiftAsync();
                break;
            case MenuEnums.Shift.DELETESHIFT:
                await DeleteShiftAsync();
                break;
            case MenuEnums.Shift.BACK:
                return true;
        }
        return false;
    }

    private async Task CreateShiftAsync()
    {
        ShiftDto shift = GetData.GetShift();
        await _shiftService.CreateShiftAsync(shift);
    }

    private async Task ReadShiftAsync()
    {
        List<Shift> shifts = await _shiftService.GetAllShiftsOfWorkerAsync();
        DisplayTable.Shift(shifts);
    }

    private async Task UpdateShiftAsync()
    {
        List<Shift> shifts = await _shiftService.GetAllShiftsOfWorkerAsync();
        Shift shiftToUpdate = GetData.GetShift(shifts);
        
        ShiftDto updatedShiftInfo = GetData.GetShift(shiftToUpdate);
        await _shiftService.UpdateShiftAsync(shiftToUpdate.Id, updatedShiftInfo);
    }

    private async Task DeleteShiftAsync()
    {
        List<Shift> shifts = await _shiftService.GetAllShiftsOfWorkerAsync();
        Shift shiftToDelete = GetData.GetShift(shifts);

        await _shiftService.DeleteShiftAsync(shiftToDelete.Id);
    }
}