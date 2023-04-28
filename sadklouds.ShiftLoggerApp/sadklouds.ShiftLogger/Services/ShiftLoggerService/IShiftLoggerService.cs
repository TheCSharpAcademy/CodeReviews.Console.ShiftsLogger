namespace sadklouds.ShiftLogger.Services.ShiftLoggerService;
public interface IShiftLoggerService
{
    public Task<List<GetShiftDto>> GetShifts();
    public Task<GetShiftDto> GetShiftById(int id);
    public Task<List<GetShiftDto>> AddShift(AddShiftDto newShift);
    public Task<GetShiftDto> UpdateShift(int id, UpdateShiftDto updatedShift);
    public Task<List<GetShiftDto>> DeleteShift(int id);
}
