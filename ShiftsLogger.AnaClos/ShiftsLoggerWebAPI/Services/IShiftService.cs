using ShiftsLoggerWebAPI.DTOs;

namespace ShiftsLoggerWebAPI.Services;

public interface IShiftService
{
    public List<ShiftDto>? GetAllShifts();
    public List<ShiftDto>? Get10ShiftsByEmployee(int id);
    public ShiftDto? GetShiftById(int id);
    public string? CreateShift(ShiftDto shiftDto);
    public string? UpdateShift(ShiftDto updatedShift);
    public string? DeleteShift(int id);
}