using ShiftLoggerApi.Dtos;

namespace ShiftLoggerApi.Data;

public interface IDataAccess
{
    public Task<List<ShiftReadDto>> GetShiftsAsync();
    public Task<ShiftReadDto?> GetShiftByIdAsync(int id);
    public Task<ShiftReadDto> AddShiftAsync(ShiftWriteDto shiftDto);
    public Task<int> UpdateShiftAsync(int id, ShiftUpdateDto shiftDto);
    public Task<int> DeleteShiftAsync(int id);
}