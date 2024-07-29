using ShiftsLogger.Dejmenek.UI.Models;
using ShiftsLogger.Dejmenek.UI.Services.Interfaces;

namespace ShiftsLogger.Dejmenek.UI.Controllers;
public class ShiftsController
{
    private readonly IShiftService _shiftService;

    public ShiftsController(IShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    public async Task AddShift()
    {
        await _shiftService.AddShift();
    }

    public async Task UpdateShift()
    {
        await _shiftService.UpdateShift();
    }

    public async Task RemoveShift()
    {
        await _shiftService.DeleteShift();
    }

    public async Task<List<ShiftReadDTO>?> GetShifts()
    {
        return await _shiftService.GetAllShifts();
    }
}
