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
        var shifts = await _apiRepo.GetShifts();
        return shifts;
    }
}