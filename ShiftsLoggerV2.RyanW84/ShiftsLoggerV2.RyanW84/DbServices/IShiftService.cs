using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using ShiftsLoggerV2.RyanW84.Models.FilterOptions;

namespace ShiftsLoggerV2.RyanW84.Services;

public interface IShiftService
{
    public Task<List<Shifts?>> GetAllShifts(ShiftFilterOptions shiftOptions);
    public Task<List<Shifts?>> GetShiftById(int id);
    public Task<Shifts> CreateShift(ShiftApiRequestDto shift);
    public Task<Shifts?> UpdateShift(int id, ShiftApiRequestDto updatedShift);
    public Task<string?> DeleteShift(int id);
}
