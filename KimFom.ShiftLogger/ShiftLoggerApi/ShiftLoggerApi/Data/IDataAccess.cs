using ShiftLoggerApi.Dtos;

namespace ShiftLoggerApi.Data;

public interface IDataAccess
{
    public Task<List<ShiftReadDto>> GetShiftsAsync();
    public Task<ShiftReadDto> GetShiftByIdAsync(int id);
    public Task AddShiftAsync(ShiftWriteDto shiftDto);
    public Task UpdateShiftAsync(int id, ShiftUpdateDto shiftDto);
    public Task DeleteShiftAsync(int id);
}