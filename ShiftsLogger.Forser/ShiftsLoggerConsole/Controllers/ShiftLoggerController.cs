using ShiftsLoggerConsole.API;

internal class ShiftLoggerController
{
    private ShiftLoggerApiAccess _apiRepo;

    public ShiftLoggerController(ShiftLoggerApiAccess apiRepo)
    {
        _apiRepo = apiRepo;
    }
    public async Task<IEnumerable<Shift>> GetShifts()
    {
        return await _apiRepo.GetShifts();
    }
    public async Task<Shift> GetShift(int id)
    {
        return await _apiRepo.GetShift(id);
    }
    public async Task<bool> PostShift(ShiftDto newShift)
    {
        return await _apiRepo.PostShift(newShift);
    }
    internal async Task<bool> DeleteShift(int selectedShift)
    {
        return await _apiRepo.DeleteShift(selectedShift);
    }
    internal async Task<bool> UpdateShift(ShiftDto newShift)
    {
        return await _apiRepo.UpdateShift(newShift);
    }
}