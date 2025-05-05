using ShiftsLogger.Ryanw84.Dtos;
using ShiftsLogger.Ryanw84.Models;

namespace ShiftsLogger.Ryanw84.Services;

public interface IShiftService
    {
    public Task<ApiResponseDto<List<Shift>>> GetAllShifts(ShiftOptions shiftoptions);
    public Task<ApiResponseDto<Shift?>> GetShiftById(int id);
    public Task<ApiResponseDto<Shift>> CreateShift(ShiftApiRequestDto shift);
    public Task<ApiResponseDto<Shift?>> UpdateShift(int id , ShiftApiRequestDto updatedShift);
    public Task<ApiResponseDto<string?>> DeleteShift(int id);
    }
