namespace sadklouds.ShiftLogger.Services.ShiftLoggerService;

public interface IShiftLoggerService
{
    public Task<ServiceResponse<List<GetShiftDto>>> GetShifts();
    public Task<ServiceResponse<GetShiftDto>> GetShiftById(int id);
    public Task<ServiceResponse<List<GetShiftDto>>> AddShift(AddShiftDto newShift);
    public Task<ServiceResponse<GetShiftDto>> UpdateShift(UpdateShiftDto updatedShift);
    public Task<ServiceResponse<List<GetShiftDto>>> DeleteShift(int id);
}
