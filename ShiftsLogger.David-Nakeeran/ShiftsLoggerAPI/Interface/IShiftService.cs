using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Interface;

public interface IShiftService
{
    public Task<ServiceResponse<List<Shift>>> GetAllShiftsAsync();

    public Task<ServiceResponse<Shift>> GetShiftByIdAsync(long id);

    public Task<ServiceResponse<Shift>> CreateShift(ShiftDTO shiftDTO);

    public Task<ServiceResponse<Shift>> UpdateShift(long id, ShiftDTO shiftDTO);

    public Task<ServiceResponse<bool>> DeleteShift(long id);
}